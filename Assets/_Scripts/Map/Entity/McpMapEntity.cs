using Shapes;
using UnityEngine;

namespace Map.Entity
{
    public class McpMapEntity : MapEntity
    {
        [SerializeField] private Rectangle _background;

        protected override void Start()
        {
            base.Start();
            _background.Color = Random.ColorHSV();
        }
    }
}