﻿using System;
using System.Collections.Generic;
using Commons;
using Commons.Types;
using Shapes;
using UI.InformationPanel;
using UnityEngine;

namespace Map.Entity
{
    public class DriverMapEntity : MapEntity
    {
        private static event Action SelectedEntityChanged;
        private static DriverMapEntity currentlySelectedDriverMapEntity = null;

        public static DriverMapEntity CurrentlySelectedDriverMapEntity
        {
            get => currentlySelectedDriverMapEntity;
            set
            {
                currentlySelectedDriverMapEntity = value;
                SelectedEntityChanged?.Invoke();
            }
        }

        [SerializeField] Polyline _routePolyline;
        private MapboxDirectionResponse _mapboxDirectionResponse;

        protected override void Start()
        {
            base.Start();
            _routePolyline.transform.SetParent(transform.parent);
            _routePolyline.transform.position = Vector3.zero;
            _routePolyline.transform.localScale = Vector3.one;

            MapEntityController.Instance.MapUpdated += () => UpdateRoutePolyline(_mapboxDirectionResponse);
            SelectedEntityChanged += CheckEnablePolyline;
            CheckEnablePolyline();
            FilterPanel.Instance.FilterChanged += EvaluateVisibility;
        }

        private void OnDestroy()
        {
            SelectedEntityChanged -= CheckEnablePolyline;
        }

        private void EvaluateVisibility()
        {
            gameObject.SetActive(FilterPanel.Instance.FilterFlags[4]);
        }
        
        public void UpdateRoutePolyline(MapboxDirectionResponse mapboxDirectionResponse)
        {
            if (mapboxDirectionResponse == null) return;
            _mapboxDirectionResponse = mapboxDirectionResponse;

            List<Vector2> points = new List<Vector2>();
            var currentCoordinate = MapEntityController.Instance.GetWorldPosition(Coordinate);
            points.Add(new Vector2(currentCoordinate.x, currentCoordinate.z));

            if (mapboxDirectionResponse.Routes == null || mapboxDirectionResponse.Routes.Count == 0) return;
            
            foreach (var coordinate in mapboxDirectionResponse.Routes[0].Geometry.Coordinates)
            {
                var point = MapEntityController.Instance.GetWorldPosition(new Coordinate(coordinate));
                points.Add(new Vector2(point.x, point.z));
            }
            _routePolyline.SetPoints(points);
        }

        protected override void ButtonClickHandler()
        {
            base.ButtonClickHandler();
            if (CurrentlySelectedDriverMapEntity == this)
            {
                CurrentlySelectedDriverMapEntity = null;
            }
            else
            {
                CurrentlySelectedDriverMapEntity = this;
            }

            InformationPanelController.Instance.ShowWorkerPanel(MapEntityController.Instance.VehicleLocationResponse.Result[_id]);
        }

        public void ShowPolyline()
        {
            _routePolyline.gameObject.SetActive(true);
        }

        public void HidePolyline()
        {
            _routePolyline.gameObject.SetActive(false);
        }

        private void CheckEnablePolyline()
        {
            if (CurrentlySelectedDriverMapEntity == this)
            {
                ShowPolyline();
            }
            else
            {
                HidePolyline();
            }
        }
    }
}