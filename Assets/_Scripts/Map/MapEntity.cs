using System;
using Types;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class MapEntity : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _button;

        private Coordinate _coordinate;

        public Coordinate Coordinate => _coordinate;

        private void Start()
        {
            MapEntityController.Instance.MapUpdated += UpdatePosition;
            _button.onClick.AddListener(ButtonClickHandler);
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
    }
}