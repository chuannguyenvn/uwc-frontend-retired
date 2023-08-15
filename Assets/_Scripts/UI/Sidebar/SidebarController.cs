using System;
using System.Collections.Generic;
using UI.Sidebar.SidePanel;
using UI.Sidebar.SidePanel.Mcp;
using UI.Sidebar.SidePanel.Messaging;
using UI.Sidebar.SidePanel.Reports;
using UI.Sidebar.SidePanel.Settings;
using UI.Sidebar.SidePanel.Vehicles;
using UI.Sidebar.SidePanel.Workers;
using UnityEngine;

namespace UI.Sidebar
{
    public class SidebarController : Singleton<SidebarController>
    {
        [SerializeField] private WorkersPrimarySidePanel _workersPrimarySidePanel;
        [SerializeField] private McpsPrimarySidePanel _mcpsPrimarySidePanel;
        [SerializeField] private VehiclesPrimarySidePanel _vehiclesPrimarySidePanel;
        [SerializeField] private ReportPrimarySidePanel _reportPrimarySidePanel;
        [SerializeField] private MessagingPrimarySidePanel _messagingPrimarySidePanel;
        [SerializeField] private SettingsPrimarySidePanel _settingsPrimarySidePanel;

        private readonly Stack<PrimarySidePanel> _backStack = new();
        public Dictionary<SidePanelType, PrimarySidePanel> Panels;

        public Dictionary<SidePanelType, string> TypeNames;

        public SidePanelType CurrentlyActivatedSidePanelType;
        public event Action<SidePanelType> SidePanelActivated;

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
                {SidePanelType.Settings, "Settings"}
            };

            Panels = new Dictionary<SidePanelType, PrimarySidePanel>
            {
                {SidePanelType.Map, null},
                {SidePanelType.Workers, _workersPrimarySidePanel},
                {SidePanelType.Mcps, _mcpsPrimarySidePanel},
                {SidePanelType.Vehicles, _vehiclesPrimarySidePanel},
                {SidePanelType.Reports, _reportPrimarySidePanel},
                {SidePanelType.Messaging, _messagingPrimarySidePanel},
                {SidePanelType.Settings, _settingsPrimarySidePanel}
            };
        }

        private void Start()
        {
            PopAllBackStack();
        }

        public void PushBackStack(PrimarySidePanel primarySidePanel)
        {
            PopAllBackStack();
            CurrentlyActivatedSidePanelType = primarySidePanel.SidePanelType;
            SidePanelActivated?.Invoke(primarySidePanel.SidePanelType);
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
            CurrentlyActivatedSidePanelType = SidePanelType.Map;
            SidePanelActivated?.Invoke(SidePanelType.Map);
            while (_backStack.Count > 0) PopBackStack();
        }
    }
}