using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core;
using Core.Aspects.Autofac.Validation;
using Core.Common;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthenticationDal _authenticationDal;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IAuthenticationDal authenticationDal, IUserService userService, IUserProfileService userProfileService, IRoleService roleService, IUserRoleService userRoleService, IEmailTemplateService emailTemplateService, IEmailService emailService, ITokenHelper tokenHelper)
        {
            _authenticationDal = authenticationDal;
            _userService = userService;
            _userProfileService = userProfileService;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _tokenHelper = tokenHelper;
        }

        public User Authenticate(User user)
        {
            return _authenticationDal.Get(a => a.IdentityNumber == user.IdentityNumber);
        }

        [ValidationAspect(typeof(RegisterValidator), Priority = 1)]
        //[SecuredOperation("Admin")]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            string password = CommonFunction.RandomPasswordString();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new Core.Entities.Concrete.User();

            user.IdentityNumber = userForRegisterDto.IdentityNumber;
            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Email = userForRegisterDto.Email;

            var newUser = _userService.Add(user);

            UserProfile userProfile = new UserProfile();
            userProfile.FirstName = userForRegisterDto.FirstName;
            userProfile.LastName = userForRegisterDto.LastName;
            userProfile.IdentityNumber = userForRegisterDto.IdentityNumber;
            userProfile.UserId = newUser.UserId;
            userProfile.Email = userForRegisterDto.Email;

            UserProfile profile = _userProfileService.Add(userProfile);

            var role = _roleService.GetByName("user");
            if (role == null)
            {
                role = _roleService.Add(new Core.Entities.Concrete.Role { Name = "User" });
            }

            UserRole userRole = new UserRole
            {
                UserId = newUser.UserId,
                RoleId = role.Id
            };

            _userRoleService.Add(userRole);

            SendEmail(userForRegisterDto.IdentityNumber, password, userForRegisterDto.Email);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> ForgotPassword(UserDTOs userDTOs)
        {
            var user = _userService.GetByIdentityNumber(userDTOs.IdentityNumber);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (user.Email.ToLower() != userDTOs.Email)
                return new ErrorDataResult<User>(Messages.UserEmailError);


            string password = CommonFunction.RandomPasswordString();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userService.Update(user);

            SendEmail(user.IdentityNumber, password, user.Email);

            return new SuccessDataResult<User>(user, Messages.UserPasswordReset);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByIdentityNumber(userForLoginDto.IdentityNumber);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Password, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string identitynumber)
        {
            if (_userService.GetByIdentityNumber(identitynumber) != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetRoles(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public bool IsValid(string identitynumber, string password)
        {
            /*Kullanıcı varsa parolası Şifrelensin bir nevi MD5 gibi düşünebiliriz*/

            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<Security>(LockerKey);

            bool result = false;

            //var tmpUser = _authenticationDal.Get(a => a.IdentityNumber == identitynumber);// Veri Tabanında kullanıcı var mı 
            //if (tmpUser == null)
            //{
            //    return new ErrorDataResult<User>(Messages.UserNotFound);
            //}

            //if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            //{
            //    return new ErrorDataResult<User>(Messages.PasswordError);
            //}

            return result;
        }

        private void SendEmail(string identity, string password, string emailaddress)
        {
            #region Send Email
            EmailTemplate emailTemplate = _emailTemplateService.GetById(1);
            string mailTemplate = _emailTemplateService.GetTemplateContent(emailTemplate);
            string subject = emailTemplate.Subject;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("UserName", identity);
            parameters.Add("Password", password);
            parameters.Add("Link", "this.BaseUrl" + "/Auth/Login");

            string end = mailTemplate;
            foreach (KeyValuePair<string, string> parametreler in parameters)
            {
                end = end.Replace(string.Concat("#", parametreler.Key, "#"), parametreler.Value);
            }

            Email email = new Email();
            email.To = emailaddress;
            email.Subject = subject;
            email.IsHtml = true;
            email.Body = end;

            _emailService.SendEmail(email);
            #endregion
        }

    }
}
