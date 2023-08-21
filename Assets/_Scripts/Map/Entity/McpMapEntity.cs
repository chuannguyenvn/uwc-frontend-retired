using System.Collections.Generic;
using Shapes;
using UI.InformationPanel;
using UnityEngine;

namespace Map.Entity
{
    public class McpMapEntity : MapEntity
    {
        public static readonly List<McpMapEntity> SelectedMcps = new();

        [SerializeField] private Rectangle _background;
        [SerializeField] private Disc _disc;
        private float _fillPercentage;

        protected override void Start()
        {
            base.Start();
            _disc.gameObject.SetActive(false);
            FilterPanel.Instance.FilterChanged += () => EvaluateVisibility(_fillPercentage);
        }

        public void UpdateCapacity(float fillPercentage)
        {
            _fillPercentage = fillPercentage;
            _background.Color = _disc.Color = VisualManager.Instance.GetFillLevelColor(fillPercentage);
            EvaluateVisibility(fillPercentage);
        }

        private void EvaluateVisibility(float fillPercentage)
        {
            if (fillPercentage < 0.9f)
            {
                gameObject.SetActive(FilterPanel.Instance.FilterFlags[0]);
            }
            else if (fillPercentage < 1f)
            {
                gameObject.SetActive(FilterPanel.Instance.FilterFlags[1]);
            }
            else
            {
                gameObject.SetActive(FilterPanel.Instance.FilterFlags[2]);
            }
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