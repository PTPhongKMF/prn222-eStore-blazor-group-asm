using System.Text.Json;
using BLL.DTOs;
using Microsoft.AspNetCore.Http;

namespace eStore.Components.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string USER_KEY = "CurrentUser";

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetUserSession(MemberDTO user)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.SetString(USER_KEY, JsonSerializer.Serialize(user));
            }
        }

        public MemberDTO? GetUserSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var userJson = session?.GetString(USER_KEY);
            
            return userJson == null ? null : JsonSerializer.Deserialize<MemberDTO>(userJson);
        }

        public void ClearUserSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Remove(USER_KEY);
        }
    }
}