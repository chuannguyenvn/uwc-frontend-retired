using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        [SerializeField] MapEntity _entityPrefab;

        List<GameObject> _spawnedObjects;
        private List<MapEntity> _mcpMapEntities = new();
        private Dictionary<int, MapEntity> _vehicleMapEntitiesById = new();

        private Timer _vehicleLocationRefreshTimer;

        private IEnumerator Start()
        {
            _abstractMap.OnUpdated += () => MapUpdated?.Invoke();

            var initializeMcpsCoroutine = InitializeMcps();
            var initializeVehiclesCoroutine = InitializeVehicles();

            yield return new WaitForAll(this, initializeMcpsCoroutine, initializeVehiclesCoroutine);

            MapUpdated?.Invoke();

            // _vehicleLocationRefreshTimer = new Timer(RefreshVehicles, this, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            
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
                            var dot = Instantiate(_entityPrefab, transform);
                            dot.Init(value.Latitude, value.Longitude);
                            _mcpMapEntities.Add(dot);
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
                            var dot = Instantiate(_entityPrefab, transform);
                            _vehicleMapEntitiesById.Add(value.Id, dot);
                        }
                    }
                },
                bearerKey: "");
        }

        private void RefreshVehicles()
        {
            Debug.Log("Refreshing vehicles.");
            StartCoroutine(HttpClient.SendRequest<GetAllVehicleLocationResponse>(endpoint: Endpoints.Vehicle.GET_ALL_LOCATION,
                requestRequestType: HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success)
                    {
                        foreach (var (vehicleId, vehicleLocation) in result.Result)
                        {
                            _vehicleMapEntitiesById[vehicleId].UpdateCoordinate(vehicleLocation);
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
            _vehicleLocationRefreshTimer.Dispose();
        }
    }
}