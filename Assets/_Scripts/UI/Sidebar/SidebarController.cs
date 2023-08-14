using System.Collections.Generic;
using UnityEngine;

namespace UI.Sidebar
{
    public class SidebarController : Singleton<SidebarController>
    {
        [SerializeField] private McpsPrimarySidePanel _mcpsPrimarySidePanel;
        [SerializeField] private VehiclesPrimarySidePanel _vehiclesPrimarySidePanel;

        public SidePanelType CurrentlyActivatedSidebarType;

        private readonly Stack<PrimarySidePanel> _backStack = new();
        public Dictionary<SidePanelType, PrimarySidePanel> Panels;

        public Dictionary<SidePanelType, string> TypeNames;

        protected override void Awake()
        {
            base.Awake();

            TypeNames = new Dictionary<SidePanelType, string>
            {
                {SidePanelType.Map, "Map"},
                {SidePanelType.Workers, "Workers"},
                {SidePanelType.Mcps, "Mcps"},
                {SidePanelType.Vehicles, "Vehicles"},
                {SidePanelType.Reports, "Reports"},
                {SidePanelType.Messaging, "Messaging"},
                {SidePanelType.Settings, "Settings"},
            };

            Panels = new Dictionary<SidePanelType, PrimarySidePanel>
            {
                {SidePanelType.Map, null},
                {SidePanelType.Workers, null},
                {SidePanelType.Mcps, _mcpsPrimarySidePanel},
                {SidePanelType.Vehicles, _vehiclesPrimarySidePanel},
                {SidePanelType.Reports, null},
                {SidePanelType.Messaging, null},
                {SidePanelType.Settings, null},
            };
        }

        public void PushBackStack(PrimarySidePanel primarySidePanel)
        {
            CurrentlyActivatedSidebarType = primarySidePanel.SidePanelType;
            PopAllBackStack();
            primarySidePanel.ShowTweened();
            _backStack.Push(primarySidePanel);
        }

        public void PopBackStack()
        {
            var inspector = _backStack.Pop();
            inspector.HideTweened();
        }

        public void PopAllBackStack()
        {
            CurrentlyActivatedSidebarType = SidePanelType.Map;
            while (_backStack.Count > 0) PopBackStack();
        }
    }
}