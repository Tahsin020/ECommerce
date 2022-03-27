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
    public class EfCargoCompanyDal : EfEntityRepositoryBase<CargoCompany, ECommerceContext>, ICargoCompanyDal
    {
        public List<CargoCompanyDetailDto> GetCargoCompanyDetails(Expression<Func<CargoCompanyDetailDto, bool>> filter = null)
        {
            using (var context = new ECommerceContext())
            {
                var result = from ca in context.CargoCompanies
                             join u in context.Users
                             on ca.UserId equals u.UserId
                             select new CargoCompanyDetailDto
                             {
                                 CargoCompanyId = ca.CargoCompanyId,
                                 UserId = u.UserId,
                                 CompanyName = ca.CompanyName,
                                 Email = u.Email,
                                 PhoneNumber = ca.PhoneNumber,
                                 Address = ca.Address,
                                 Description = ca.Description,
                                 Status = ca.Status
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();

            }
        }
    }
}
