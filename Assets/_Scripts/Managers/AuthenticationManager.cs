using Commons.Communications.Account;
using Https;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public int UserId = 1;
        public string JwtToken;

        public void TryLogin(string username, string password)
        {
            StartCoroutine(HttpClient.SendRequest<int>(Endpoints.Account.LOGIN,
                HttpClient.RequestType.POST,
                (success, result) =>
                {
                    if (success)
                    {
                        UserId = result;
                        SceneManager.LoadScene(1);
                    }
                },
                "",
                new LoginRequest {Username = username, Password = password}));
        }
    }
}