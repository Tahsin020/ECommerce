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
    public class EfProductDal : EfEntityRepositoryBase<Product, ECommerceContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter=null)
        {
            using (var context=new ECommerceContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             join co in context.Colors
                             on p.ColorId equals co.ColorId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 ColorName = co.ColorName,
                                 UnitPrice = p.UnitPrice,
                                 StockQuantity = p.StockQuantity,
                                 Description = p.Description,
                                 Status = p.Status,
                                 ProductImages = ((from pi in context.ProductImages
                                                   where (p.ProductId == pi.ProductId)
                                                   select new ProductImage
                                                   {
                                                       Id = pi.Id,
                                                       ProductId = pi.ProductId,
                                                       ImagePath = pi.ImagePath,
                                                       Date = pi.Date
                                                   }).ToList()).Count == 0
                                                   ? new List<ProductImage> { new ProductImage { Id = -1, ProductId = p.ProductId, Date = DateTime.Now, ImagePath = "/images/default.jpg" } }
                                                   : (from pi in context.ProductImages
                                                      where (p.ProductId == pi.ProductId)
                                                      select new ProductImage
                                                      {
                                                          Id=pi.Id,
                                                          ProductId=pi.ProductId,
                                                          ImagePath=pi.ImagePath,
                                                          Date=pi.Date
                                                      }).ToList()
                          
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
