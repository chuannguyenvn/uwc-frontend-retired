using Shapes;
using UnityEngine;

namespace Map.Entity
{
    public class McpMapEntity : MapEntity
    {
        [SerializeField] private Rectangle _background;

        public void UpdateCapacity(float fillPercentage)
        {
            _background.Color =   VisualManager.Instance.GetFillLevelColor(fillPercentage);
        }
    }
}