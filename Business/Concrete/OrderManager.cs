using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly ICreditCardService _creditCardService;

        public OrderManager(IOrderDal orderDal, IProductService productService, IPaymentService paymentService, ICreditCardService creditCardService)
        {
            _orderDal = orderDal;
            _productService = productService;
            _paymentService = paymentService;
            _creditCardService = creditCardService;
        }

        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(Messages.AddedOrder);
        }

        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(Messages.DeletedOrder);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(),Messages.OrdersListed);
        }

        public IDataResult<List<Order>> GetByCargoCompanyId(int cargoCompanyId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.CargoCompanyId == cargoCompanyId), Messages.OrdersListed);
        }

        public IDataResult<List<Order>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.CustomerId == customerId), Messages.OrdersListed);
        }

        public IDataResult<Order> GetById(int id)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(o => o.OrderId == id), Messages.OrdersListed);
        }

        public IDataResult<List<Order>> GetByProductId(int productId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.ProductId == productId), Messages.OrdersListed);
        }

        public IDataResult<List<OrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetailDtos(), Messages.OrdersListed);
        }

        public IDataResult<List<OrderDetailDto>> GetOrdersByCustomerIdWithDetails(int customerId)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetailDtos(o => o.CustomerId == customerId), Messages.OrdersListed);
        }

        public IDataResult<List<OrderDetailDto>> GetOrdersByStatusWithDetails(bool status)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetailDtos(o => o.Status == status), Messages.OrdersListed);
        }

        public IDataResult<int> Order(OrderPaymentRequestModel orderPaymentRequest)
        {
            var creditCardResult = _creditCardService.Get(orderPaymentRequest.CardNumber, orderPaymentRequest.ExpireYear, orderPaymentRequest.ExpireMonth, orderPaymentRequest.Cvc, orderPaymentRequest.CardHolderFullName.ToUpper());

            List<Order> verifiedOrders = new List<Order>();
            decimal totalAmount = 0;

            if (creditCardResult.Success)
            {
                //Verify Orders
                foreach (var order in orderPaymentRequest.Orders)
                {
                    //Get Amount
                    var productUnitPrice = _productService.GetById(order.ProductId).Data.UnitPrice;
                    var amount = productUnitPrice * order.Total;
                    totalAmount += amount;
                }

                //Check Total Amount
                if (totalAmount != orderPaymentRequest.Amount)
                {
                    return new ErrorDataResult<int>(-1, Messages.TotalAmountNotMatch);
                }

                //Pay
                var creditCard = creditCardResult.Data;
                var paymentResult = _paymentService.Pay(creditCard, orderPaymentRequest.CustomerId, orderPaymentRequest.Amount);

                //Verify payment
                if (paymentResult.Success && paymentResult.Data != -1)
                {
                    //Add orders on db
                    foreach (var verifiedOrder in verifiedOrders)
                    {
                        verifiedOrder.PaymentId = paymentResult.Data;

                        //Add order
                        var orderAddResult = Add(verifiedOrder);

                        //Check order
                        if (!orderAddResult.Success)
                        {
                            return new ErrorDataResult<int>(-1, orderAddResult.Message);
                        }
                    }
                    return new SuccessDataResult<int>(paymentResult.Data, Messages.OrderSuccessful);
                }
                return new ErrorDataResult<int>(-1, paymentResult.Message);
            }
            return new ErrorDataResult<int>(-1, creditCardResult.Message);
        }

        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.UpdatedOrder);
        }
    }
}
