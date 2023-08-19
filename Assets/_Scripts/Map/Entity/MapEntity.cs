using Commons.Types;
using UnityEngine;

namespace Map.Entity
{
    public abstract class MapEntity : MonoBehaviour
    {
        public int _id;

        public int Id => _id;
        public Coordinate Coordinate { get; private set; }
        public float Rotation { get; private set; }

        protected virtual void Start()
        {
            MapEntityController.Instance.MapUpdated += UpdatePositionAndRotation;
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

        public void UpdateCoordinate(Coordinate coordinate, float rotation)
        {
            Coordinate = coordinate;
            Rotation = rotation;
            UpdatePositionAndRotation();
        }

        public void UpdateCoordinate(double latitude, double longitude)
        {
            Coordinate = new Coordinate(latitude, longitude);
        }

        private void UpdatePositionAndRotation()
        {
            transform.position = MapEntityController.Instance.GetWorldPosition(Coordinate);
            transform.rotation = Quaternion.Euler(0, Rotation, 0);
        }

        protected virtual void ButtonClickHandler()
        {
            Debug.Log("Clicked.");
        }
    }
}