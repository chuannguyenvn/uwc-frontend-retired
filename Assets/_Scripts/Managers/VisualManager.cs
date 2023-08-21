using System;
using System.Collections.Generic;
using Commons.Types;
using TMPro;
using UnityEngine;

public class VisualManager : PersistentSingleton<VisualManager>
{
    public event Action ThemeChanged;
    public event Action<bool> DarkModeToggled;

    public float SIDE_PANEL_TRANSITION_TIME;

    public TMP_FontAsset PrimaryTextFont;
    public Color PrimaryTextColor;

    public TMP_FontAsset SecondaryTextFont;
    public Color SecondaryTextColor;

    public Color PrimaryPanelColor;
    public Color SecondaryPanelColor;
    public Color BackgroundPanelColor;

    public Color McpNotFullColor;
    public Color McpNearlyFullColor;
    public Color McpFullColor;

    public List<Color> Themes;

    public bool IsDarkMode = false;

    public Color GetFillLevelColor(float fillPercentage)
    {
        return fillPercentage switch
        {
            < 0.9f => McpNotFullColor,
            < 1f => McpNearlyFullColor,
            _ => McpFullColor
        };
    }

    public string GetFillLevelDescription(float fillPercentage)
    {
        return fillPercentage switch
        {
            < 0.9f => "Not full",
            < 1f => "Nearly full",
            _ => "Full"
        };
    }

    public string GetVehicleTypeString(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.FrontLoader => "Frontloader",
            VehicleType.SideLoader => "Sideloader",
            VehicleType.RearLoader => "Rearloader",
            _ => "Unknown"
        };
    }

    public void ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
        if (IsDarkMode)
        {
            BackgroundPanelColor = new Color(0.05f, 0.05f, 0.05f, 1f);
        }
        else
        {
            BackgroundPanelColor = Color.white;
        }
        DarkModeToggled?.Invoke(IsDarkMode);
        ThemeChanged?.Invoke();
    }

    public void SetTheme(int themeOrder)
    {
        PrimaryPanelColor = Themes[themeOrder];
        ThemeChanged?.Invoke();
    }
}