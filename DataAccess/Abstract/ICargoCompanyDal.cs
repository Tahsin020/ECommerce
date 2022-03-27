using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICargoCompanyDal : IEntityRepository<CargoCompany>
    {
        List<CargoCompanyDetailDto> GetCargoCompanyDetails(Expression<Func<CargoCompanyDetailDto,bool>> filter=null);
    }
}
