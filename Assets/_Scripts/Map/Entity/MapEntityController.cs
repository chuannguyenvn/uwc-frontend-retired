using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Commons.Communications.Vehicle;
using Commons.Types;
using Https;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Models;
using UnityEngine;

namespace Map.Entity
{
    public class MapEntityController : Singleton<MapEntityController>
    {
        [SerializeField] private AbstractMap _abstractMap;

        [SerializeField] private McpMapEntity _mcpMapEntityPrefab;
        [SerializeField] private DriverMapEntity _driverMapEntityPrefab;
        [SerializeField] private CleanerMapEntity _cleanerMapEntityPrefab;
        private readonly Dictionary<int, CleanerMapEntity> _cleanerMapEntitiesById = new();
        public readonly Dictionary<int, DriverMapEntity> DriverMapEntitiesById = new();
        private readonly Dictionary<int, McpMapEntity> _mcpMapEntitiesById = new();

        public GetAllVehicleLocationResponse VehicleLocationResponse { get; private set; }

        private Timer _vehicleLocationRefreshTimer;

        private IEnumerator Start()
        {
            _abstractMap.OnUpdated += () => MapUpdated?.Invoke();

            var initializeMcpsCoroutine = InitializeMcps();
            var initializeVehiclesCoroutine = InitializeVehicles();

            yield return new WaitForAll(this, initializeMcpsCoroutine, initializeVehiclesCoroutine);

            MapUpdated?.Invoke();

            InvokeRepeating(nameof(RefreshVehicles), 1f, 1f);
            InvokeRepeating(nameof(RefreshMcps), 1f, 1f);
        }

        private void OnDestroy()
        {
            _vehicleLocationRefreshTimer?.Dispose();
        }

        public event Action MapUpdated;

        private IEnumerator InitializeMcps()
        {
            return HttpClient.SendRequest<List<Mcp>>(Endpoints.Mcp.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                        foreach (var value in result)
                        {
                            var dot = Instantiate(_mcpMapEntityPrefab, transform);
                            dot.InitId(value.Id);
                            dot.InitCoordinate(value.Latitude, value.Longitude);

                            _mcpMapEntitiesById.Add(value.Id, dot);
                        }
                },
                "");
        }

        private IEnumerator InitializeVehicles()
        {
            return HttpClient.SendRequest<List<Vehicle>>(Endpoints.Vehicle.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                        foreach (var value in result)
                        {
                            var dot = Instantiate(_driverMapEntityPrefab, transform);
                            dot.InitId(value.Id);
                            DriverMapEntitiesById.Add(value.Id, dot);
                        }
                },
                "");
        }

        private void RefreshMcps()
        {
            StartCoroutine(HttpClient.SendRequest<List<Mcp>>(Endpoints.Mcp.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                        foreach (var value in result)
                        {
                            _mcpMapEntitiesById[value.Id].UpdateCapacity(value.Capacity);
                        }
                },
                ""));
        }
        
        private void RefreshVehicles()
        {
            StartCoroutine(HttpClient.SendRequest<GetAllVehicleLocationResponse>(Endpoints.Vehicle.GET_ALL_LOCATION,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                    {
                        VehicleLocationResponse = result;

                        foreach (var (vehicleId, vehicleMovement) in result.Result)
                        {
                            DriverMapEntitiesById[vehicleId]
                                .UpdateCoordinate(vehicleMovement.CurrentLocation, vehicleMovement.CurrentOrientationAngle);
                            DriverMapEntitiesById[vehicleId].UpdateRoutePolyline(vehicleMovement.MapboxDirectionResponse);
                        }
                    }
                },
                ""));
        }

        public Vector3 GetWorldPosition(Coordinate coordinate)
        {
            return _abstractMap.GeoToWorldPosition(new Vector2d(coordinate.Latitude, coordinate.Longitude));
        }
    }
}