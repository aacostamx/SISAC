//------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Newtonsoft.Json;
    using Resources;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models;
    using VOI.SISAC.Web.Models.VO.Security;
    using WebMatrix.WebData;
    using System.Text;
    /// <summary>
    /// Account controller
    /// </summary>
    [CustomAuthorize]
    public class AccountController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.Login;

        /// <summary>
        /// The module business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userBusiness">The user business.</param>
        public AccountController(IUserBusiness userBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.userBusiness = userBusiness;
        }

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Action result</returns>
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login(string returnUrl)
        {
            HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="user">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVO user, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(user);
            }

            bool domainUser = false;
            bool userValidated = false;

            UserDto userDto = new UserDto();
            this.ValidateUser(user, ref userValidated, ref domainUser, ref userDto);

            if (userValidated && user != null)
            {
                this.SetupFormsAuthenticationTicket(userDto, user.RememberMe);
                return this.RedirectToLocal(returnUrl);
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resource.LoginFailed);
            }

            return this.View(user);
        }

        /// <summary>
        /// Log off.
        /// </summary>
        /// <returns>Action result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            this.RemoveCookie("UserPermissions");
            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Change language.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult ChangeLanguage(string language, string returnUrl)
        {
            HttpCookie culture = Request.Cookies["_culture"];
            string userLanguage = this.IsValidLanguage(language) ? language : Resource.SpanishCulture;
            if (culture != null)
            {
                culture.Value = language;
                Response.Cookies.Set(culture);
            }
            else
            {
                this.Response.Cookies.Add(
                    new HttpCookie("_culture", userLanguage)
                    {
                        Expires = DateTime.Now.AddDays(5),
                        HttpOnly = true
                    });
            }

            return this.RedirectToLocal(returnUrl);
        }

        /// <summary>
        /// Determines whether the language is valid.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if is valid otherwise <c>false</c>.</returns>
        private bool IsValidLanguage(string language)
        {
            if (language == null)
            {
                return false;
            }

            if (language.Contains(Resource.SpanishCulture) || language.Contains(Resource.EnglishCulture))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Action result.</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Set ups the forms authentication ticket.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="persistanceFlag">If set to <c>true</c> persist the user log in.</param>
        private void SetupFormsAuthenticationTicket(UserDto user, bool persistanceFlag)
        {
            try
            {
                UserData userData = new UserData();
                string json = string.Empty;

                userData.Roles = this.ModulePerimissioToString(user.ModulePermissions.ToList());
                userData.AirportsAlloed = this.GetAirportsFromUser(user.UserAirports);
                json = JsonConvert.SerializeObject(userData, Formatting.None);

                // The authentication ticket cookie
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(user.UserName, persistanceFlag, 30);
                string encryptTicket = FormsAuthentication.Encrypt(authenticationTicket);
                this.Response.Cookies.Add(
                     new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket)
                     {
                         Expires = authenticationTicket.Expiration,
                         Path = FormsAuthentication.FormsCookiePath,
                         HttpOnly = true
                     });

                // Permissions for user cookie
                string compress = CompressHelper.CompressToString(json);
                string encrypt = EncryptHelper.EncryptString(compress);

                this.RemoveCookie("UserPermissions");
                HttpCookie cookie = new HttpCookie("UserPermissions", encrypt)
                {
                    Path = FormsAuthentication.FormsCookiePath,
                    Expires = DateTime.Now.AddDays(1),
                    HttpOnly = true
                };
                this.HttpContext.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Removes the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        private void RemoveCookie(string cookieName)
        {
            try
            {
                if (Request.Cookies[cookieName] != null)
                {
                    var c = new HttpCookie(cookieName);
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userValidated">if set to <c>true</c> [user validated].</param>
        /// <param name="domainUser">if set to <c>true</c> [domain user].</param>
        /// <param name="userDto">The user data transfer object.</param>
        private void ValidateUser(LoginVO user, ref bool userValidated, ref bool domainUser, ref UserDto userDto)
        {
            string passwordDecrypted = string.Empty;
            if (!string.IsNullOrWhiteSpace(user.UserName) || !string.IsNullOrWhiteSpace(user.Password))
            {
                domainUser = AuthenticateUserHelper.isDomainUser(user.UserName, user.Password);
                userDto = this.GetUserInfo(user.UserName);

                if (domainUser && userDto != null)
                {
                    userValidated = true;
                }
                else
                {
                    if (userDto != null && !string.IsNullOrEmpty(userDto.PasswordEncripted)
                        && userDto.UserName.Equals(user.UserName)
                        && userDto.Status)
                    {
                        passwordDecrypted = EncryptHelper.DecryptString(userDto.PasswordEncripted);
                        userValidated = passwordDecrypted.Equals(user.Password) ? true : false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User information</returns>
        private UserDto GetUserInfo(string userName)
        {
            try
            {
                UserDto userDto = new UserDto();
                userDto = this.userBusiness.GetUserByUserName(userName);
                return userDto;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }

        /// <summary>
        /// Modules and permission to string.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <returns>Array of string with roles for the user.</returns>
        private string[] ModulePerimissioToString(List<ModulePermissionDto> permissions)
        {
            List<string> algo = new List<string>();
            foreach (ModulePermissionDto item in permissions)
            {
                algo.Add(item.ModuleCode + "-" + item.PermissionCode);
            }

            return algo.ToArray();
        }

        /// <summary>
        /// Gets the airports of the user logged in.
        /// </summary>
        /// <param name="airports">The permissions.</param>
        /// <returns>Array of string with roles for the user.</returns>
        private List<string> GetAirportsFromUser(IList<UserAirportDto> airports)
        {
            List<string> airport = new List<string>();
            foreach (UserAirportDto item in airports)
            {
                airport.Add(item.StationCode);
            }

            return airport;
        }
    }
}