using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Login
{
    public class LoginController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private Button _loginButton;

        private void Start()
        {
            _loginButton.onClick.AddListener(LoginWithCredentials);
        }

        private void LoginWithCredentials()
        {
            var username = _usernameInputField.text;
            var password = _passwordInputField.text;
            AuthenticationManager.Instance.TryLogin(username, password);
        }
    }
}