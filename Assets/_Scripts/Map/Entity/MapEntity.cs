using Types;
using UnityEngine;

namespace Map.Entity
{
    public abstract class MapEntity : MonoBehaviour
    {
        public int _id;

        public int Id => _id;
        public Coordinate Coordinate { get; private set; }

        protected virtual void Start()
        {
            MapEntityController.Instance.MapUpdated += UpdatePosition;
        }

        private void OnMouseUp()
        {
            ButtonClickHandler();
        }

        public void InitId(int id)
        {
            _id = id;
        }

        public void InitCoordinate(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void InitCoordinate(double latitude, double longitude)
        {
            Coordinate = new Coordinate(latitude, longitude);
        }

        public void UpdateCoordinate(Coordinate coordinate)
        {
            Coordinate = coordinate;
            UpdatePosition();
        }

        public void UpdateCoordinate(double latitude, double longitude)
        {
            Coordinate = new Coordinate(latitude, longitude);
        }

        private void UpdatePosition()
        {
            transform.position = MapEntityController.Instance.GetWorldPosition(Coordinate);
        }

        protected virtual void ButtonClickHandler()
        {
            Debug.Log("Clicked.");
        }
    }
}