using System.Threading;
using System.Web;


namespace Resources.Resources.App_LocalResources
{
	public class AdminResource
	{
        private static global::System.Resources.ResourceManager resourceMan;
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static global::System.Resources.ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceMan, null)) 
				{
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.App_LocalResources.AdminResource", typeof(AdminResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Returns the formatted resource string.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static string GetResourceString(string key, params string[] tokens)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            var str = ResourceManager.GetString(key, culture);

			for(int i = 0; i < tokens.Length; i += 2)
				str = str.Replace(tokens[i], tokens[i+1]);
										
            return str;
        }
        
        /// <summary>
        ///   Returns the formatted resource string.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static HtmlString GetResourceHtmlString(string key, params string[] tokens)
		{
			var str = GetResourceString(key, tokens);
							
			if(str.StartsWith("HTML:"))
				str = str.Substring(5);

			return new HtmlString(str);
        }
		
		public static string Action { get { return GetResourceString("Action"); } }

		public static string ActionRequire { get { return GetResourceString("ActionRequire"); } }

		public static string AdminLayout_ActionLink_DeptAdmin { get { return GetResourceString("AdminLayout_ActionLink_DeptAdmin"); } }

		public static string AdminLayout_ActionLink_UserAdmin { get { return GetResourceString("AdminLayout_ActionLink_UserAdmin"); } }

		public static string AdminLayout_View_AdminText { get { return GetResourceString("AdminLayout_View_AdminText"); } }

		public static string AdminLayout_View_FrontEndText { get { return GetResourceString("AdminLayout_View_FrontEndText"); } }

		public static string ApplicationRole_Description { get { return GetResourceString("ApplicationRole_Description"); } }

		public static string Controller { get { return GetResourceString("Controller"); } }

		public static string DepartmentViewModelId { get { return GetResourceString("DepartmentViewModelId"); } }

		public static string DepartmentViewModelName { get { return GetResourceString("DepartmentViewModelName"); } }

		public static string PermissionViewModel_Description { get { return GetResourceString("PermissionViewModel_Description"); } }

		public static class Names
		{

			/// <summary>
			/// Action
			/// </summary>
			public const string Action = "Action";

			/// <summary>
			/// ActionRequire
			/// </summary>
			public const string ActionRequire = "ActionRequire";

			/// <summary>
			/// AdminLayout_ActionLink_DeptAdmin
			/// </summary>
			public const string AdminLayout_ActionLink_DeptAdmin = "AdminLayout_ActionLink_DeptAdmin";

			/// <summary>
			/// AdminLayout_ActionLink_UserAdmin
			/// </summary>
			public const string AdminLayout_ActionLink_UserAdmin = "AdminLayout_ActionLink_UserAdmin";

			/// <summary>
			/// AdminLayout_View_AdminText
			/// </summary>
			public const string AdminLayout_View_AdminText = "AdminLayout_View_AdminText";

			/// <summary>
			/// AdminLayout_View_FrontEndText
			/// </summary>
			public const string AdminLayout_View_FrontEndText = "AdminLayout_View_FrontEndText";

			/// <summary>
			/// ApplicationRole_Description
			/// </summary>
			public const string ApplicationRole_Description = "ApplicationRole_Description";

			/// <summary>
			/// Controller
			/// </summary>
			public const string Controller = "Controller";

			/// <summary>
			/// DepartmentViewModelId
			/// </summary>
			public const string DepartmentViewModelId = "DepartmentViewModelId";

			/// <summary>
			/// DepartmentViewModelName
			/// </summary>
			public const string DepartmentViewModelName = "DepartmentViewModelName";

			/// <summary>
			/// PermissionViewModel_Description
			/// </summary>
			public const string PermissionViewModel_Description = "PermissionViewModel_Description";
		}
	}
}
