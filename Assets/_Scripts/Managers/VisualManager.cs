using Commons.Types;
using TMPro;
using UnityEngine;

public class VisualManager : PersistentSingleton<VisualManager>
{
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
}