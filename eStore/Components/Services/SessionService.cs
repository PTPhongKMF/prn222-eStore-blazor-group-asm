using System.Text.Json;
using BLL.DTOs;
using Microsoft.AspNetCore.Http;

namespace eStore.Components.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string USER_KEY = "CurrentUser";

        public SessionService(IHttpContextAccessor httpContextAccessor) => 
            _httpContextAccessor = httpContextAccessor;

        public void SetUserSession(MemberDTO user) => 
            _httpContextAccessor.HttpContext?.Session?.SetString(USER_KEY, JsonSerializer.Serialize(user));

        public MemberDTO? GetUserSession()
        {
            var userJson = _httpContextAccessor.HttpContext?.Session?.GetString(USER_KEY);
            return userJson != null ? JsonSerializer.Deserialize<MemberDTO>(userJson) : null;
        }

        public void ClearUserSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Remove(USER_KEY);
        }
    }
}