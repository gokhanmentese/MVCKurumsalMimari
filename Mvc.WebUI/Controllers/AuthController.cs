using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Concrete;
using Business.Constants;
using Core;
using Core.Common;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;
using Core.Extensions;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Mvc.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        //private readonly IHostEnvironment _hostEnvironment;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public AuthController(IUserService userService, IUserProfileService userProfileService, IAuthService authenticationService, IEmailService emailService, IEmailTemplateService emailTemplateService, IUserRoleService userRoleService, IRoleService roleService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _authService = authenticationService;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public IActionResult Login()
        {
            return View(new UserViewModel());
        }

        //[Route("/login")]
        //public ActionResult LoginProb()
        //{
        //    return View("Login");
        //}

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            UserForLoginDto userForLoginDto = new UserForLoginDto
            {
                IdentityNumber = user.IdentityNumber,
                Password = user.Password
            };

            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                ModelState.AddModelError("Hata", userToLogin.Message);
            }

            if (ModelState.IsValid)
            {
                //var result = _authService.CreateAccessToken(userToLogin.Data);
                //if (result.Success)
                //{
                //    return RedirectToAction("Index", "Home");
                //}

                #region Cookkie Authentication

                var userRoles = _userService.GetRoles(userToLogin.Data);
                var userProfile = _userProfileService.GetByUserId(userToLogin.Data.UserId);

                var claims = new List<Claim>();
                claims.AddNameIdentifier(userToLogin.Data.UserId.ToString());
                claims.AddEmail(userProfile.Email);
                claims.AddName($"{userProfile.FirstName} {userProfile.LastName}");
                claims.AddRoles(userRoles.Select(c => c.Name).ToArray());

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(userIdentity));
                #endregion

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        [Authorize]
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();

            registerViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Kullanıcılar", Controller = "User", Action = "ShowUsers" },
                Link3 = new PageLink { DisplayName = "Yeni Kullanıcı", Controller = "Auth", Action = "Register" }
            };

            return View(registerViewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            registerViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Kullanıcılar", Controller = "User", Action = "ShowUsers" },
                Link3 = new PageLink { DisplayName = "Yeni Kullanıcı", Controller = "Auth", Action = "Register" }
            };

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Hata", "Kullanıcı Üyelik İşlemleri Sırasında Hata Meydana Geldi");

                return View(registerViewModel);
            }

            try
            {
                var userExists = _authService.UserExists(registerViewModel.IdentityNumber);
                if (!userExists.Success)
                {
                    UserForRegisterDto userForRegisterDto = new UserForRegisterDto
                    {
                        IdentityNumber = registerViewModel.IdentityNumber,
                        Email = registerViewModel.Email,
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName
                    };

                    var registerResult = _authService.Register(userForRegisterDto);
                    //var result = _authService.CreateAccessToken(registerResult.Data);
                    if (registerResult.Success)
                    {
                        return RedirectToAction("ShowUsers", "User");
                    }
                    else
                        ModelState.AddModelError("Hata", registerResult.Message);
                }
                else
                    ModelState.AddModelError("Hata", userExists.Message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Hata", "İşlem sırasında hata alındı");
            }

            return View(registerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }

        public IActionResult AccessDenied()
        {
            //if (Page.Request.UrlReferrer != null && Page.Request.UrlReferrer.AbsoluteUri.ToLower().Contains("login.aspx"))
            //{
            //    FormsAuthentication.SignOut();
            //    Roles.DeleteCookie();
            //    Session.Clear();
            //}

            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                Error = "Bu sayfaya yetkiniz yoktur!"
            };

            return View(errorViewModel);
        }

        public IActionResult ForgotPassword()
        {
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Şifremi Unuttum", Controller = "Auth", Action = "ForgotPassword" }
            };

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserViewModel us)
        {
            //if (!ModelState.IsValid)
            //    return View(us);

            /*Kullanıcı Email ve Parola Doğru ise*/

            var userExists = _authService.UserExists(us.IdentityNumber);
            if (!userExists.Success)
            {
                return NotFound(userExists.Message);
            }

            UserDTOs userDTOs = new UserDTOs
            {
                IdentityNumber = us.IdentityNumber,
                Email = us.Email
            };

            var registerResult = _authService.ForgotPassword(userDTOs);

            if (registerResult.Success)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
                ModelState.AddModelError("Hata", registerResult.Message);

            return View(us);
        }


    }
}
