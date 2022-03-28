using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetUserById(int userId)
        {
            var user = _userDal.Get(u => u.UserId == userId);
            if (user != null)
            {
                return new SuccessDataResult<User>(user, Messages.UsersListed);
            }
            return new ErrorDataResult<User>(Messages.UserNotExist);
        }

        public IResult Add(User user)
        {
            var result = BusinessRules.Run(CheckIfEmailExist(user.Email));
            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(int userId)
        {
            var result = BusinessRules.Run(CheckIfUserIdExist(userId));
            if (result != null)
            {
                return result;
            }

            var deletedUser = _userDal.Get(u => u.UserId == userId);
            _userDal.Delete(deletedUser);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult Update(User user)
        {
            var result = BusinessRules.Run(CheckIfUserIdExist(user.UserId), CheckIfEmailAvailable(user.Email));
            if (result != null)
            {
                return result;
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = BusinessRules.Run(CheckIfUserIdExist(user.UserId));
            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<User> GetUserByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        //BusinessRules

        private IResult CheckIfUserIdExist(int userId)
        {
            var result = _userDal.GetAll(u => u.UserId == userId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEmailExist(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (result)
            {
                return new ErrorResult(Messages.UserEmailExist);
            }
            return new SuccessResult();
        }
        private bool BaseCheckIfEmailExist(string userEmail)
        {
            return _userDal.GetAll(u => u.Email == userEmail).Any();
        }

        private IResult CheckIfEmailAvailable(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (!result)
            {
                return new ErrorResult(Messages.UserEmailNotAvailable);
            }
            return new SuccessResult();
        }

    }
}
