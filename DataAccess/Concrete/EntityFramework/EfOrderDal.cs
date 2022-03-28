using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, ECommerceContext>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetailDtos(Expression<Func<OrderDetailDto, bool>> filter = null)
        {
            using (var context = new ECommerceContext())
            {
                var result = from o in context.Orders
                             join c in context.Customers
                             on o.CustomerId equals c.Id
                             join u in context.Users
                             on c.UserId equals u.UserId
                             join p in context.Products
                             on o.ProductId equals p.ProductId
                             join ca in context.CargoCompanies
                             on o.CargoCompanyId equals ca.CargoCompanyId
                             join pa in context.Payments
                             on o.PaymentId equals pa.Id
                             select new OrderDetailDto
                             {
                                 OrderId = o.OrderId,
                                 UserId = c.UserId,
                                 CustomerId = c.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 ProductName = p.ProductName,
                                 ProductQuantity = o.ProductQuantity,
                                 CargoCompanyName = ca.CompanyName,
                                 Total = o.Total,
                                 Amount = pa.Amount,
                                 OrderDate = o.OrderDate,
                                 DeliveryDate = o.DeliveryDate,
                                 Descriptinon = o.Description,
                                 Status = o.Status
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
