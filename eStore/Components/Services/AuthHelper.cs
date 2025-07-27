using BLL.DTOs;

namespace eStore.Components.Services
{
    public static class AuthHelper
    {
        public const string AdminEmail = "admin@estore.com";
        
        public static bool IsAdmin(MemberDTO? user) => 
            user?.Email?.Equals(AdminEmail, StringComparison.OrdinalIgnoreCase) == true;
            
        public static string GetDisplayName(MemberDTO? user) => 
            user?.Email?.Split('@')[0] ?? string.Empty;
    }
}