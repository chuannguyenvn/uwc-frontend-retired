using Types;
using UnityEngine;

namespace Map.Entity
{
    public class MapEntity : MonoBehaviour
    {
        private Coordinate _coordinate;

        public Coordinate Coordinate => _coordinate;

        private void Start()
        {
            MapEntityController.Instance.MapUpdated += UpdatePosition;
        }

        public void Init(Coordinate coordinate)
        {
            _coordinate = coordinate;
        }

        public void Init(double latitude, double longitude)
        {
            _coordinate = new Coordinate(latitude, longitude);
        }

        public void UpdateCoordinate(Coordinate coordinate)
        {
            _coordinate = coordinate;
            UpdatePosition();
        }

        public void UpdateCoordinate(double latitude, double longitude)
        {
            _coordinate = new Coordinate(latitude, longitude);
        }

        private void UpdatePosition()
        {
            transform.position = MapEntityController.Instance.GetWorldPosition(_coordinate);
        }

        protected virtual void ButtonClickHandler()
        {
            Debug.Log("Clicked.");
        }

        private void OnMouseUp()
        {
            ButtonClickHandler();
        }
    }
}