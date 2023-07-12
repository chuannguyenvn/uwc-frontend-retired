using System.Collections;
using Communications.Authentication;
using Https;
using UnityEngine;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public string JwtToken;

        public void TryLogin(string username, string password)
        {
            StartCoroutine(HttpClient.SendRequest(endpoint: Endpoints.DOMAIN + Endpoints.ACCOUNT + Endpoints.LOGIN,
                requestType: HttpClient.Type.POST,
                (success) => { Debug.Log(success); },
                bearerKey: "",
                objectToSend: new LoginRequest() {Username = username, Password = password}));
        }
    }
}