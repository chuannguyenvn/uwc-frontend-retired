using Types;
using UnityEngine;

namespace Map.Entity
{
    public abstract class MapEntity : MonoBehaviour
    {
        public int _id;
        public int Id => _id;

        private Coordinate _coordinate;
        public Coordinate Coordinate => _coordinate;

        protected virtual void Start()
        {
            MapEntityController.Instance.MapUpdated += UpdatePosition;
        }

        public void InitId(int id)
        {
            _id = id;
        }

        public void InitCoordinate(Coordinate coordinate)
        {
            _coordinate = coordinate;
        }

        public void InitCoordinate(double latitude, double longitude)
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