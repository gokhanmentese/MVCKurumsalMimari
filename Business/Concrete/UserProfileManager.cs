using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using MernisServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserProfileManager : IUserProfileService
    {
        private readonly IUserProfileDal _userProfileDal;

        public UserProfileManager(IUserProfileDal userProfileDal)
        {
            _userProfileDal = userProfileDal;
        }


        public UserProfile Add(UserProfile user)
        {
            if (_userProfileDal.Get(b => b.IdentityNumber == user.IdentityNumber) == null)
            {
                return _userProfileDal.Add(user);
            }
            else
            {
                throw new Exception("Bu kullanıcı daha önceden sisteme eklenmiştir.");
            }
        }

        public void Delete(UserProfile user)
        {
            _userProfileDal.Delete(user);
        }

        [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public List<UserProfile> GetAll()
        {
            return _userProfileDal.GetList().ToList();
        }

        public UserProfile GetById(Guid id)
        {
            return _userProfileDal.Get(b => b.Id == id);
        }

        [CacheAspect(duration: 10)]
        public List<UserProfile> GetByUnitId(int unitid)
        {
            return _userProfileDal.GetList(u => u.UnitId == unitid).ToList();
        }

        public UserProfile GetByUserId(Guid userid)
        {
            return _userProfileDal.Get(b => b.UserId == userid);
        }

        public void Update(UserProfile user)
        {
            _userProfileDal.Update(user);
        }

        public bool CheckUser(UserProfile userProfile)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            return client.TCKimlikNoDogrulaAsync(Convert.ToInt64(userProfile.IdentityNumber), userProfile.FirstName, userProfile.LastName, userProfile.YearOfBirth).Result.Body.TCKimlikNoDogrulaResult;
        }

        public List<UserProfile> GetUsersForAssignedTask(Guid taskid)
        {
            return _userProfileDal.GetUsersForAssignedTask(taskid);
        }
    }
}
