using System.Collections.Generic;
using UnityEngine;

namespace UI.Sidebar
{
    public class SidebarController : Singleton<SidebarController>
    {
        [SerializeField] private TasksPrimarySidePanel _tasksPrimarySidePanel;
        [SerializeField] private McpsPrimarySidePanel _mcpsPrimarySidePanel;
        [SerializeField] private VehiclesPrimarySidePanel _vehiclesPrimarySidePanel;

        public Dictionary<SidePanelType, string> TypeNames;
        public Dictionary<SidePanelType, PrimarySidePanel> Panels;

        public SidePanelType CurrentlyActivatedSidebarType;

        private readonly Stack<PrimarySidePanel> _backStack = new();

        protected override void Awake()
        {
            base.Awake();

            TypeNames = new()
            {
                {SidePanelType.Map, "Map"},
                {SidePanelType.Tasks, "Tasks"},
                {SidePanelType.Workers, "Workers"},
                {SidePanelType.Mcps, "Mcps"},
                {SidePanelType.Vehicles, "Vehicles"},
                {SidePanelType.Reports, "Reports"},
                {SidePanelType.Messaging, "Messaging"},
                {SidePanelType.Settings, "Settings"},
                {SidePanelType.Helps, "Helps"},
            };

            Panels = new()
            {
                {SidePanelType.Map, null},
                {SidePanelType.Tasks, _tasksPrimarySidePanel},
                {SidePanelType.Workers, null},
                {SidePanelType.Mcps, _mcpsPrimarySidePanel},
                {SidePanelType.Vehicles, _vehiclesPrimarySidePanel},
                {SidePanelType.Reports, null},
                {SidePanelType.Messaging, null},
                {SidePanelType.Settings, null},
                {SidePanelType.Helps, null},
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