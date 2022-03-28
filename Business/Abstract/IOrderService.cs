using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int id);
        IDataResult<List<Order>> GetByProductId(int productId);
        IDataResult<List<Order>> GetByCustomerId(int customerId);
        IDataResult<List<Order>> GetByCargoCompanyId(int cargoCompanyId);
        IDataResult<List<OrderDetailDto>> GetOrderDetails();
        IDataResult<List<OrderDetailDto>> GetOrdersByCustomerIdWithDetails(int customerId);
        IDataResult<List<OrderDetailDto>> GetOrdersByStatusWithDetails(bool status);
        IDataResult<int> Order(OrderPaymentRequestModel orderPaymentRequest);
        IResult Add(Order order);
        IResult Delete(Order order);
        IResult Update(Order order);
    }
}
