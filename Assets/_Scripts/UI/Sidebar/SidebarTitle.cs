using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SidebarTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void ChangeTitle(string title)
    {
        _text.text = title;
    }
}
