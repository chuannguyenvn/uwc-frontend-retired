using System.Collections.Generic;
using Shapes;
using UI.InformationPanel;
using UnityEngine;

namespace Map.Entity
{
    public class McpMapEntity : MapEntity
    {
        public readonly static List<McpMapEntity> SelectedMcps = new();

        [SerializeField] private Rectangle _background;
        [SerializeField] private Disc _disc;

        protected override void Start()
        {
            base.Start();
            _disc.gameObject.SetActive(false);
        }

        public void UpdateCapacity(float fillPercentage)
        {
            _background.Color = _disc.Color = VisualManager.Instance.GetFillLevelColor(fillPercentage);
        }

        protected override void ButtonClickHandler()
        {
            base.ButtonClickHandler();
            if (WorkerInformationPanel.IsAssigning)
            {
                if (SelectedMcps.Contains(this))
                {
                    SelectedMcps.Remove(this);
                    _disc.gameObject.SetActive(false);
                }
                else
                {
                    SelectedMcps.Add(this);
                    _disc.gameObject.SetActive(true);
                }
            }
        }
    }
}