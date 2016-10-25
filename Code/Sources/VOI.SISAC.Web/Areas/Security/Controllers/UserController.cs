//------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Security;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// User Controller
    /// </summary>
    [CustomAuthorize]    
	public class UserController : BaseController
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ModuleController));

		/// <summary>
		/// The user information
		/// </summary>
		private readonly string userInfo;

		/// <summary>
		/// Catalog name
		/// </summary>
		private readonly string catalogName = "User";

		/// <summary>
		/// The primary key
		/// </summary>
		private readonly string primaryKey = "UserID";

        /// <summary>
        /// The user business
        /// </summary>
		private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The generic catalog business
        /// </summary>
		private readonly IGenericCatalogBusiness genericCatalogBusiness;

        /// <summary>
        /// The airport business
        /// </summary>
		private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The profilerole business
        /// </summary>
        private readonly IProfileRoleBusiness profileroleBusiness;

        /// <summary>
        /// The role business
        /// </summary>
        private readonly IRoleBusiness roleBusiness;

        /// <summary>
        /// The module business
        /// </summary>
        private readonly IModulePermissionBusiness moduleBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userBusiness">The user business.</param>
        /// <param name="genericCatalogBusiness">The generic catalog business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="profileroleBusiness">The profilerole business.</param>
        /// <param name="roleBusiness">The role business.</param>
        /// <param name="moduleBusiness">The module business.</param>
		public UserController(
            IUserBusiness userBusiness, 
            IGenericCatalogBusiness genericCatalogBusiness,
            IAirportBusiness airportBusiness,
            IProfileRoleBusiness profileroleBusiness, 
            IRoleBusiness roleBusiness, 
            IModulePermissionBusiness moduleBusiness)
		{
			this.userInfo = string.Format(
			   LogMessages.UserInfo,
			   System.Environment.UserDomainName,
			   System.Environment.UserName,
			   System.Environment.MachineName);
			this.userBusiness = userBusiness;
			this.genericCatalogBusiness = genericCatalogBusiness;
			this.airportBusiness = airportBusiness;
            this.roleBusiness = roleBusiness;
            this.moduleBusiness = moduleBusiness;
            this.profileroleBusiness = profileroleBusiness;
		}

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "USER-IDX")]
		public ActionResult Index()
		{
			Trace.TraceInformation("Iniciando Index User");
			IList<UserDto> userDto = new List<UserDto>();
			IList<UserVO> userVO = new List<UserVO>();
			try
			{
				Trace.TraceInformation("Antes de Consulta en BD User");
				userDto = this.userBusiness.GetActiveUsers();
				userVO = Mapper.Map<IList<UserDto>, IList<UserVO>>(userDto);
				Trace.TraceInformation("Despues de Consulta en BD User");
			}
			catch (BusinessException exception)
			{
				Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
				Logger.Error(exception.Message, exception);
				Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
				Trace.TraceError(exception.Message, exception);
				this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
			}

			return this.View(userVO);
		}

        /// <summary>
        /// Create get
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "USER-ADD")]
		public ActionResult Create()
		{
			LoadCatalogs();
			return this.View();
		}

        /// <summary>
        /// Create Post
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "USER-ADD")]
		public ActionResult Create(UserVO user)
		{
			if (user == null)
			{
				Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
				Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			try
			{
                UserDto userDto = new UserDto();
                //Encriptar password
                if(user != null)
                user.PasswordEncripted = Helpers.EncryptHelper.EncryptString(user.PasswordEncripted);

                if (String.IsNullOrEmpty(user.PasswordEncripted))
                {
                    user.PasswordEncripted = null;
                }

                user.CreationDate = DateTime.Now;
                userDto = Mapper.Map<UserVO, UserDto>(user);
                
                this.userBusiness.AddUser(userDto);
			    this.TempData["OperationSuccess"] = Resources.Resource.SuccessCreate;
                return Json("Success");
			}
			catch (BusinessException exception)
			{
				// Sets information of the error.
				Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
				Logger.Error(exception.Message, exception);
				Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
				Trace.TraceError(exception.Message, exception);
				string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
				if (exception.Number == 10)
				{
					message = string.Format(message, primaryKey);
                    return Json("10");
				}

				this.ViewBag.ErrorMessage = message;
                return Json("An Error Has occoured: " + exception.ToString());
			}
		}

        /// <summary>
        /// Edit get.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "USER-UPD")]
		public ActionResult Edit(long id)
		{
            LoadCatalogs();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {                
                UserDto userDto = this.userBusiness.FindUserById(id);
                //desencriptar password
                if (userDto != null)
                userDto.PasswordEncripted = Helpers.EncryptHelper.DecryptString(userDto.PasswordEncripted);
                
                UserVO userVo = Mapper.Map<UserDto, UserVO>(userDto);
                if (userVo == null)
                    return HttpNotFound();

                return this.View(userVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return this.View();
            }
		}

        /// <summary>
        /// Edit Post
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "USER-UPD")]
        public ActionResult Edit(UserVO user)
        {
            if (user == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                UserDto userDto = new UserDto();
                //Encriptar password
                user.PasswordEncripted = Helpers.EncryptHelper.EncryptString(user.PasswordEncripted);

                if (String.IsNullOrEmpty(user.PasswordEncripted))
                {
                    user.PasswordEncripted = null;
                }

                user.CreationDate = DateTime.Now;
                userDto = Mapper.Map<UserVO, UserDto>(user);

                this.userBusiness.UpdateUser(userDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                return Json("Success");
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, primaryKey);
                    return Json("10");
                }

                this.ViewBag.ErrorMessage = message;
                return Json("An Error Has occoured: " + exception.ToString());
            }
        }

        /// <summary>
        /// Delete main view
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "USER-DEL")]
		public ActionResult Delete(long id)
		{
            UserVO userVo = new UserVO();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                UserDto userDto = this.userBusiness.FindUserById(id);
                userVo = Mapper.Map<UserDto, UserVO>(userDto);
                if (userDto == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);

            }

            return View(userVo);
		}


        /// <summary>
        /// Delete Confirmed
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "USER-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            UserVO userVO = new UserVO();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                UserDto userDto = this.userBusiness.FindUserById(id);
                userVO = Mapper.Map<UserDto, UserVO>(userDto);
                this.userBusiness.DeleteUser(userDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(userVO);
        }

        /// <summary>
        /// Get Modules Permissions
        /// </summary>
        /// <param name="profileRole">The profile role.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        public JsonResult GetModulesPermissions(List<UserProfileRoleVO> profileRole)
        {
            RoleDto roleDto = new RoleDto();
            bool edit = false;
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            string value = string.Empty;
            IList<string> list = new List<string>();
            try
            {                
                List<ModulePermissionVO> result = new List<ModulePermissionVO>();
                result = GetModulePermission(profileRole);

                if (result != null && result.Count() > 0)
                {
                    edit = result.Count() > 0 ? false : true;
                }

                foreach (var item in result)
                {
                    value = item.ModuleCode + "/" + item.PermissionCode;
                    list.Add(value);
                }

                jsonConvert.Edit = edit;
                jsonConvert.DataValue = list;

                json = JsonConvert.SerializeObject(jsonConvert, Formatting.None, new JsonSerializerSettings());

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Modules Permissions
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Json object.</returns>
        [HttpGet]
        public JsonResult GetModulesPermissionsFromDataBase(long UserID)
        {
            RoleDto roleDto = new RoleDto();
            bool edit = true;
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            string value = string.Empty;
            IList<string> list = new List<string>();
            try
            {
                List<ModulePermissionVO> result = new List<ModulePermissionVO>();

                UserDto userDto = this.userBusiness.FindUserById(UserID);
                UserVO userVo = Mapper.Map<UserDto, UserVO>(userDto);

                result = userVo.ModulePermissions.ToList();

                if (result != null && result.Count() > 0)
                {
                    edit = result.Count() > 0 ? false : true;
                }

                foreach (var item in result)
                {
                    value = item.ModuleCode + "/" + item.PermissionCode;
                    list.Add(value);
                }

                jsonConvert.Edit = edit;
                jsonConvert.DataValue = list;

                json = JsonConvert.SerializeObject(jsonConvert, Formatting.None, new JsonSerializerSettings());

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <returns>Object with the permission.</returns>
        [HttpGet]
        public JsonResult GetWarningMessageConfiguration()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("requiered", Resource.RequiredField);
            dictionary.Add("email", Resource.EmailVal);
            dictionary.Add("max10", Resource.LengthMax10);
            dictionary.Add("max25", Resource.LengthMax25);
            dictionary.Add("min5", Resource.LengthMin5);
            dictionary.Add("max20", Resource.LengthMax20);
            dictionary.Add("password", Resource.PasswordEqualVal);

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
		private void LoadCatalogs()
		{
			this.ViewBag.Airports = genericCatalogBusiness.GetAirportsCatalog();
			this.ViewBag.Departments = genericCatalogBusiness.GetDepartmentCatalog();
			this.ViewBag.Airlines = genericCatalogBusiness.GetAirlineCatalog();
            this.ViewBag.Profiles = genericCatalogBusiness.GetProfileCatalog();
            //tree
            IList<ModulePermissionDto> modulePermissionDto = new List<ModulePermissionDto>();
            modulePermissionDto = this.moduleBusiness.GetAllModules();

            IList<ModulePermissionVO> modulePermissionVO = new List<ModulePermissionVO>();
            modulePermissionVO = Mapper.Map<IList<ModulePermissionDto>, IList<ModulePermissionVO>>(modulePermissionDto);

            this.ViewBag.ModulePermissions = modulePermissionVO;
		}

        /// <summary>
        /// GetModulePermission
        /// </summary>
        /// <param name="profileRole"></param>
        /// <returns>List of Module and permissions</returns>
        private List<ModulePermissionVO> GetModulePermission(List<UserProfileRoleVO> profileRole) 
        {        
            // Consultar Lista de Roles
            IList<RoleDto> roleListDto = new List<RoleDto>();
            roleListDto = this.roleBusiness.GetRoles();
            List<ModulePermissionDto> modulePermissionListDto = new List<ModulePermissionDto>();

            // Recorremos lista de Roles que ha seleccionado
            foreach (UserProfileRoleVO userProfileRole in profileRole)
            {
                foreach (RoleDto item in roleListDto)
                {
                    if (item.RoleCode == userProfileRole.RoleCode)
                    {
                        modulePermissionListDto.AddRange(item.ModulePermissions.ToList());
                    }
                }
            }            

            return Mapper.Map<List<ModulePermissionDto>, List<ModulePermissionVO>>(modulePermissionListDto.Distinct().ToList<ModulePermissionDto>());
		}

        /// <summary>
        /// Gets the airports.
        /// </summary>
        /// <param name="airportID">The airport identifier.</param>
        /// <returns>Action result.</returns>
		public ActionResult GetAirports(string airportID)
		{
			AirportDto airportDto = new AirportDto();
			airportDto = this.airportBusiness.FindAirportById(airportID);
			return this.Json(airportDto, JsonRequestBehavior.AllowGet);
		}

        /// <summary>
        /// Gets the roles by profile.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>Action result.</returns>
        public ActionResult GetRolesByProfile(string profileCode)
        {
            IList<GenericCatalogDto> roleDto = new List<GenericCatalogDto>();
            roleDto = this.profileroleBusiness.FindRolesByProfileCode(profileCode, Resource.SelectItem);
            return this.Json(roleDto, JsonRequestBehavior.AllowGet);
        }
	}
}