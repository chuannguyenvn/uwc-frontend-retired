using System.Collections.Generic;
using Commons;
using Commons.Types;
using Shapes;
using UnityEngine;

namespace Map.Entity
{
    public class DriverMapEntity : MapEntity
    {
        [SerializeField] Polyline _routePolyline;
        private MapboxDirectionResponse _mapboxDirectionResponse;

        protected override void Start()
        {
            base.Start();
            _routePolyline.transform.SetParent(transform.parent);
            _routePolyline.transform.position = Vector3.zero;
            _routePolyline.transform.localScale = Vector3.one;

            MapEntityController.Instance.MapUpdated += () => UpdateRoutePolyline(_mapboxDirectionResponse);
        }

        public void UpdateRoutePolyline(MapboxDirectionResponse mapboxDirectionResponse)
        {
            if (mapboxDirectionResponse == null) return;
            _mapboxDirectionResponse = mapboxDirectionResponse;

            List<Vector2> points = new List<Vector2>();
            var currentCoordinate = MapEntityController.Instance.GetWorldPosition(Coordinate);
            points.Add(new Vector2(currentCoordinate.x, currentCoordinate.z));
            foreach (var coordinate in mapboxDirectionResponse.Routes[0].Geometry.Coordinates)
            {
                var point = MapEntityController.Instance.GetWorldPosition(new Coordinate(coordinate));
                points.Add(new Vector2(point.x, point.z));
            }
            _routePolyline.SetPoints(points);
        }
    }
}