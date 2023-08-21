using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterPanel : Singleton<FilterPanel>
{
    public List<bool> FilterFlags = new()
    {
        true,
        true,
        true,
        true,
        true
    };

    [SerializeField] private List<Button> _buttons;

    public event Action FilterChanged;

    private void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            _buttons[i]
                .onClick.AddListener(() =>
                {
                    FilterFlags[index] = !FilterFlags[index];
                    if (FilterFlags[index])
                    {
                        _buttons[index].targetGraphic.color = Color.white;
                    }
                    else
                    {
                        _buttons[index].targetGraphic.color = Color.gray;
                    }
                    FilterChanged?.Invoke();
                });
        }
    }
}