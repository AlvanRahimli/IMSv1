using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace IMSv1.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this HttpContext context)
        {
            var userId = context.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId ?? "0");
        }
        
        public static string GetUserName(this HttpContext context)
        {
            var name = context.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return name;
        }
    }
}