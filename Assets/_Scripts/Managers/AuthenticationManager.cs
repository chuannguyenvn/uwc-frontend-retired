using Communications.Authentication;
using Https;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public string JwtToken;

        public void TryLogin(string username, string password)
        {
            StartCoroutine(HttpClient.SendRequest(Endpoints.Account.LOGIN,
                HttpClient.RequestType.POST,
                success =>
                {
                    if (success) SceneManager.LoadScene(1);
                },
                "",
                new LoginRequest {Username = username, Password = password}));
        }
    }
}