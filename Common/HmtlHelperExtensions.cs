using System;
using System.Linq;
using System.Web.Mvc;

namespace Common
{
    public static class HmtlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "active";
            string currentAction = ((string)html.ViewContext.RouteData.Values["action"]).ToLower();
            string currentController = ((string)html.ViewContext.RouteData.Values["controller"]).ToLower();
            string[] arr = { "" };
            string[] arr2 = { "" }; ;

            if (String.IsNullOrEmpty(controller))
                controller = currentController;
            else
            {
                arr = controller.ToLower().Split('|');
            }

            if (String.IsNullOrEmpty(action))
                action = currentAction.ToLower();
            else
            {
                arr2 = action.ToLower().Split('|');
            }
            return (controller == currentController || arr.Contains(currentController)) &&
                   (action == currentAction || arr2.Contains(currentAction))
                ? cssClass
                : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string ConvertClass(this HtmlHelper html, int state)
        {
            if (state == 0)
            {
                return "red-bg";
            }
            else
            {
                return " ";
            }
        }
    }
}