using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        IDataResult<List<ProductImage>> GetAll();
        IDataResult<List<ProductImage>> GetProductImages(int productId);
        IDataResult<ProductImage> GetById(int imageId);
        IResult Add(IFormFile file, int productId);
        IResult Update(ProductImage productImage, IFormFile file);
        IResult Delete(ProductImage productImage);
        IResult DeleteAllImagesOfProductByProductId(int productId);
    }
}
