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
    public class CargoCompanyManager : ICargoCompanyService
    {
        ICargoCompanyDal _cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public IResult Add(CargoCompany cargoCompany)
        {
            _cargoCompanyDal.Add(cargoCompany);
            return new SuccessResult(Messages.AddedCargoCompany);
        }

        public IResult Delete(CargoCompany cargoCompany)
        {
            _cargoCompanyDal.Delete(cargoCompany);
            return new SuccessResult(Messages.DeletedCargoCompany);
        }

        public IDataResult<CargoCompany> Get(int cargoCompanyId)
        {
            return new SuccessDataResult<CargoCompany>(_cargoCompanyDal.Get(p => p.CargoCompanyId == cargoCompanyId),Messages.CargoCompanyListed);
        }

        public IDataResult<List<CargoCompany>> GetAll()
        {
            return new SuccessDataResult<List<CargoCompany>>(_cargoCompanyDal.GetAll(), Messages.CargoCompanyListed);
        }

        public IDataResult<List<CargoCompanyDetailDto>> GetCargoCompanyDetails()
        {
            return new SuccessDataResult<List<CargoCompanyDetailDto>>(_cargoCompanyDal.GetCargoCompanyDetails(), Messages.CargoCompanyListed);
        }

        public IDataResult<List<CargoCompanyDetailDto>> GetCargoCompanyDetails(int cargoCompanyId)
        {
            return new SuccessDataResult<List<CargoCompanyDetailDto>>(_cargoCompanyDal.GetCargoCompanyDetails(c => c.CargoCompanyId == cargoCompanyId), Messages.CargoCompanyListed);
        }

        public IDataResult<List<CargoCompanyDetailDto>> GetStatusByCargoCompanyDetails(bool status)
        {
            return new SuccessDataResult<List<CargoCompanyDetailDto>>(_cargoCompanyDal.GetCargoCompanyDetails(c => c.Status == status), Messages.CargoCompanyListed);
        }

        public IResult Update(CargoCompany cargoCompany)
        {
            _cargoCompanyDal.Update(cargoCompany);
            return new SuccessResult(Messages.UpdatedCargoCompany);
        }
    }
}
