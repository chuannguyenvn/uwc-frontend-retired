using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Communications.Vehicle;
using Https;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Types;
using UnityEngine;

namespace Map.Entity
{
    public class MapEntityController : Singleton<MapEntityController>
    {
        public event Action MapUpdated;

        [SerializeField] AbstractMap _abstractMap;

        [SerializeField] private McpMapEntity _mcpMapEntityPrefab;
        [SerializeField] private DriverMapEntity _driverMapEntityPrefab;
        [SerializeField] private CleanerMapEntity _cleanerMapEntityPrefab;

        private readonly Dictionary<int, McpMapEntity> _mcpMapEntitiesById = new();
        private readonly Dictionary<int, DriverMapEntity> _driverMapEntitiesById = new();
        private readonly Dictionary<int, CleanerMapEntity> _cleanerMapEntitiesById = new();

        private Timer _vehicleLocationRefreshTimer;

        private IEnumerator Start()
        {
            _abstractMap.OnUpdated += () => MapUpdated?.Invoke();

            var initializeMcpsCoroutine = InitializeMcps();
            var initializeVehiclesCoroutine = InitializeVehicles();

            yield return new WaitForAll(this, initializeMcpsCoroutine, initializeVehiclesCoroutine);

            MapUpdated?.Invoke();

            InvokeRepeating(nameof(RefreshVehicles), 1f, 5f);
        }

        private IEnumerator InitializeMcps()
        {
            return HttpClient.SendRequest<List<Models.Mcp>>(endpoint: Endpoints.Mcp.GET_ALL,
                requestRequestType: HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                    {
                        foreach (var value in result)
                        {
                            var dot = Instantiate(_mcpMapEntityPrefab, transform);
                            dot.InitId(value.Id);
                            dot.InitCoordinate(value.Latitude, value.Longitude);

                            _mcpMapEntitiesById.Add(value.Id, dot);
                        }
                    }
                },
                bearerKey: "");
        }

        private IEnumerator InitializeVehicles()
        {
            return HttpClient.SendRequest<List<Models.Vehicle>>(endpoint: Endpoints.Vehicle.GET_ALL,
                requestRequestType: HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                    {
                        foreach (var value in result)
                        {
                            var dot = Instantiate(_driverMapEntityPrefab, transform);
                            dot.InitId(value.Id);

                            _driverMapEntitiesById.Add(value.Id, dot);
                        }
                    }
                },
                bearerKey: "");
        }

        private void RefreshVehicles()
        {
            StartCoroutine(HttpClient.SendRequest<GetAllVehicleLocationResponse>(endpoint: Endpoints.Vehicle.GET_ALL_LOCATION,
                requestRequestType: HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                    {
                        foreach (var (vehicleId, vehicleLocation) in result.Result)
                        {
                            _driverMapEntitiesById[vehicleId].UpdateCoordinate(vehicleLocation);
                        }
                    }
                },
                bearerKey: ""));
        }

        public Vector3 GetWorldPosition(Coordinate coordinate)
        {
            return _abstractMap.GeoToWorldPosition(new Vector2d(coordinate.Latitude, coordinate.Longitude));
        }

        private void OnDestroy()
        {
            _vehicleLocationRefreshTimer?.Dispose();
        }
    }
}