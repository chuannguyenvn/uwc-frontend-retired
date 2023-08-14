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
}