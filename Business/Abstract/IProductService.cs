using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetByCategoryId(int categoryId);
        IDataResult<List<Product>> GetByColorId(int colorId);
        IDataResult<List<Product>> GetByStatus(bool status);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetailDto();
        IDataResult<List<ProductDetailDto>> GetProductDetailDtoById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);


    }
}
