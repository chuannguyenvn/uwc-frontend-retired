using System;
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
            
        }
    }
}