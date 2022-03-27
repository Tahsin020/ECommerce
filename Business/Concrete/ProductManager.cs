using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.AddedProduct);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.DeletedProduct);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.ColorId == colorId), Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByStatus(bool status)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.Status == status), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max), Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDto()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtoById(int productId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(p => p.ProductId == productId), Messages.ProductsListed);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.UpdatedProduct);
        }
    }
}
