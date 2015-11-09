using Resources.Resources.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAuth.Controllers;

namespace WebAuth.Areas.Admin.Controllers
{
    /// <summary>
    /// ResourceController
    /// </summary>
    /// <see cref="http://afana.me/post/aspnet-mvc-internationalization-strings-localization-client-side.aspx"/>
    public partial class ResourceController : BaseController
    {
        public virtual JsonResult GetResources()
        {
            return Json(new Dictionary<string, string> { 
                {AdminResource.Names.Action, AdminResource.Names.Action},
                {AdminResource.Names.ApplicationRole_Description,AdminResource.Names.ApplicationRole_Description},
                {AdminResource.Names.Controller, AdminResource.Names.Controller},
                   //resources.PermissionViewModel_Description
            }, JsonRequestBehavior.AllowGet);
        }
    }
}