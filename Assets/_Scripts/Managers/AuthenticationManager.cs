using System.Collections;
using Communications.Authentication;
using Https;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public string JwtToken;

        public void TryLogin(string username, string password)
        {
            StartCoroutine(HttpClient.SendRequest(endpoint: Endpoints.DOMAIN + Endpoints.Account.LOGIN,
                requestType: HttpClient.Type.POST,
                (success) =>
                {
                    if (success)
                    {
                        SceneManager.LoadScene(1);
                    }
                },
                bearerKey: "",
                objectToSend: new LoginRequest() {Username = username, Password = password}));
        }
    }
}