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
    public interface ICargoCompanyService
    {
        IDataResult<List<CargoCompany>> GetAll();
        IDataResult<CargoCompany> Get(int cargoCompanyId);
        IDataResult<List<CargoCompanyDetailDto>> GetCargoCompanyDetails();
        IDataResult<List<CargoCompanyDetailDto>> GetCargoCompanyDetails(int cargoCompanyId);
        IDataResult<List<CargoCompanyDetailDto>> GetStatusByCargoCompanyDetails(bool status);
        IResult Add(CargoCompany cargoCompany);
        IResult Delete(CargoCompany cargoCompany);
        IResult Update(CargoCompany cargoCompany);
    }
}
