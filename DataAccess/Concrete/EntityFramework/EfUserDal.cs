using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ECommerceContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context=new ECommerceContext())
            {
                var result = from userOperationClaim in context.UserOperationClaims
                             join operationCliam in context.OperationClaims
                             on userOperationClaim.OperationClaimId equals operationCliam.Id
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaim
                             {
                                 Id=operationCliam.Id,
                                 Name=operationCliam.Name
                             };
                return result.ToList();
            }
        }
    }
}
