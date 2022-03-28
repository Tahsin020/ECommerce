using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;

        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        public IResult Add(IFormFile file, int productId)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfProductImageLimitExceded(productId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var imageResult = FileHelper.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }

            ProductImage productImage = new ProductImage
            {
                ImagePath = imageResult.Message,
                ProductId = productId,
                Date = DateTime.Now
            };
            _productImageDal.Add(productImage);
            return new SuccessResult(Messages.ProductImageAdded);
        }

        public IResult Delete(ProductImage productImage)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfProductImageIdExist(productImage.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var deletedImage = _productImageDal.Get(p => p.Id == productImage.Id);
            var result = FileHelper.Delete(deletedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorDeletingImage);
            }
            _productImageDal.Delete(deletedImage);
            return new SuccessResult(Messages.ProductImageDeleted);
        }

        public IResult DeleteAllImagesOfProductByProductId(int productId)
        {
            var deletedImages = _productImageDal.GetAll(p => p.ProductId == productId);
            if (deletedImages == null)
            {
                return new ErrorResult(Messages.NoPictureOfTheProduct);
            }
            foreach (var deletedImage in deletedImages)
            {
                _productImageDal.Delete(deletedImage);
                FileHelper.Delete(deletedImage.ImagePath);
            }
            return new SuccessResult(Messages.ProductImageDeleted);
        }

        public IDataResult<ProductImage> GetById(int imageId)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(p => p.Id == imageId), Messages.ProductImageListed);
        }

        public IDataResult<List<ProductImage>> GetProductImages(int productId)
        {
            var checkIfProductImage = CheckIfProductHasImage(productId);
            var images = checkIfProductImage.Success
                ? checkIfProductImage.Data
                : _productImageDal.GetAll(p => p.ProductId == productId);
            return new SuccessDataResult<List<ProductImage>>(images, checkIfProductImage.Message);
        }

        public IDataResult<List<ProductImage>> GetAll()
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll(), Messages.ProductsImagesListed);
        }

        public IResult Update(ProductImage productImage, IFormFile file)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfProductImageIdExist(productImage.Id),
                CheckIfProductImageLimitExceded(productImage.ProductId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var updatedImage = _productImageDal.Get(p => p.Id == productImage.Id);
            var result = FileHelper.Update(file, updatedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorUpdatingImage);
            }
            productImage.ImagePath = result.Message;
            productImage.Date = DateTime.Now;
            _productImageDal.Update(productImage);
            return new SuccessResult(Messages.ProductImageUpdated);
        }

        private IResult CheckIfProductImageLimitExceded(int productId)
        {
            int result = _productImageDal.GetAll(p => p.ProductId == productId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.ProductImageLimitExceeded);
            }
            return new SuccessResult();

        }

        private IDataResult<List<ProductImage>> CheckIfProductHasImage(int productId)
        {
            string logoPath = "/images/default.jpg";
            bool result = _productImageDal.GetAll(p => p.ProductId == productId).Any();
            if (!result)
            {
                List<ProductImage> imageList = new List<ProductImage>
                {
                    new ProductImage
                    {
                        ImagePath=logoPath,
                        ProductId=productId,
                        Date=DateTime.Now
                    }
                };
                return new SuccessDataResult<List<ProductImage>>(imageList, Messages.GetDefaultImage);
            }
            return new ErrorDataResult<List<ProductImage>>(new List<ProductImage>(), Messages.ProductImagesListed);
        }

        private IResult CheckIfProductImageIdExist(int imageId)
        {
            var result = _productImageDal.GetAll(p => p.Id == imageId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ProductImageIdNotExist);
            }
            return new SuccessResult();
        }
    }
}
