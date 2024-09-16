using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Day1
{
    public class TraceProductDeleted : ActionFilterAttribute
    {
         string path = Directory.GetCurrentDirectory()
           + "/Logging/" + DateTime.Today.ToString("yy-MM-dd") + ".txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"On {DateTime.Now.ToString()}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"UserID {context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)} ");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"User Name {context.HttpContext.User.FindFirstValue(ClaimTypes.Name)} ");

            File.AppendAllText(path, stringBuilder.ToString());
        }
    }
}
