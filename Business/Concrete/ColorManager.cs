﻿using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public Result Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.AddedColor);
        }

        public Result Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DeletedColor);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c =>c.ColorId==colorId), Messages.ColorsListed);
        }

        public Result Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.UpdatedColor);
        }
    }
}
