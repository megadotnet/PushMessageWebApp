using System.Web.Mvc;

namespace WebAuth.Areas.Chat
{
    /// <summary>
    /// ChatAreaRegistration
    /// </summary>
    public class ChatAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "Chat";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chat_default",
                "Chat/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}