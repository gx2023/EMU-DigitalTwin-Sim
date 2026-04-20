
using UnityEngine;
using System.Threading.Tasks;

namespace EMU.DT.Api
{
    public class AuthService
    {
        private readonly ApiClient apiClient;
        private string authToken;
        
        public AuthService(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        
        public string AuthToken
        {
            get =&gt; authToken;
            set =&gt; authToken = value;
        }
        
        public async Task&lt;LoginResponse&gt; LoginAsync(LoginRequest request)
        {
            return await apiClient.PostAsync&lt;LoginResponse&gt;("api/auth/login", request);
        }
        
        public async Task&lt;UserResponse&gt; RegisterAsync(RegisterRequest request)
        {
            return await apiClient.PostAsync&lt;UserResponse&gt;("api/auth/register", request);
        }
        
        public async Task&lt;bool&gt; LogoutAsync()
        {
            authToken = null;
            await Task.CompletedTask;
            return true;
        }
    }
    
    [System.Serializable]
    public class LoginRequest
    {
        public string username;
        public string password;
    }
    
    [System.Serializable]
    public class LoginResponse
    {
        public bool success;
        public string token;
        public UserDto user;
    }
    
    [System.Serializable]
    public class RegisterRequest
    {
        public string username;
        public string password;
        public string email;
        public string realName;
    }
    
    [System.Serializable]
    public class UserResponse
    {
        public bool success;
        public UserDto user;
    }
    
    [System.Serializable]
    public class UserDto
    {
        public long id;
        public string username;
        public string email;
        public string realName;
    }
}
