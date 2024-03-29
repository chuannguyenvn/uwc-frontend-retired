﻿using System.Collections;
using UnityEngine;

namespace UI.Sidebar.SidePanel.Reports
{
    public class ReportPrimarySidePanel : PrimarySidePanel
    {
        private void Awake()
        {
            SidePanelType = SidePanelType.Reports;
        }

        protected override IEnumerator Start()
        {
            // Must be called before base.Start() to ensure that the _rectTransform is set
            _rectTransform.sizeDelta = _rectTransform.sizeDelta.WithX(Screen.width - 100);

            yield return base.Start();
        }
    }
}