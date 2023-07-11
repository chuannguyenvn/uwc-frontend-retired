using System;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Map
{
    public class MapNavigationController : Singleton<MapNavigationController>
    {
        [SerializeField] [Range(1, 20)] public float _panSpeed = 1.0f;
        [SerializeField] private float _zoomSpeed = 0.25f;
        [SerializeField] public Camera _referenceCamera;
        [SerializeField] private AbstractMap _mapManager;
        [SerializeField] private bool _useDegreeMethod;
        private bool _dragStartedOnUI;
        private Plane _groundPlane = new(Vector3.up, 0);
        private bool _isInitialized;
        private Vector3 _mousePosition;
        private Vector3 _mousePositionPrevious;

        private Vector3 _origin;
        private bool _shouldDrag;

        protected override void Awake()
        {
            base.Awake();
            if (null == _referenceCamera)
            {
                _referenceCamera = GetComponent<Camera>();
                if (null == _referenceCamera) Debug.LogErrorFormat("{0}: reference camera not set", GetType().Name);
            }
            _mapManager.OnInitialized += () => { _isInitialized = true; };
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()) _dragStartedOnUI = true;

            if (Input.GetMouseButtonUp(0)) _dragStartedOnUI = false;
        }

        private void LateUpdate()
        {
            if (!_isInitialized) return;

            if (!_dragStartedOnUI)
            {
                if (Input.touchSupported && Input.touchCount > 0)
                    HandleTouch();
                else
                    HandleMouseAndKeyBoard();
            }
        }

        private void HandleMouseAndKeyBoard()
        {
            // zoom
            var scrollDelta = 0.0f;
            scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            ZoomMapUsingTouchOrMouse(scrollDelta);


            //pan keyboard
            var xMove = Input.GetAxis("Horizontal");
            var zMove = Input.GetAxis("Vertical");

            PanMapUsingKeyBoard(xMove, zMove);


            //pan mouse
            PanMapUsingTouchOrMouse();
        }

        private void HandleTouch()
        {
            var zoomFactor = 0.0f;
            //pinch to zoom.
            switch (Input.touchCount)
            {
                case 1:
                {
                    PanMapUsingTouchOrMouse();
                }
                    break;
                case 2:
                {
                    // Store both touches.
                    var touchZero = Input.GetTouch(0);
                    var touchOne = Input.GetTouch(1);

                    // Find the position in the previous frame of each touch.
                    var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    zoomFactor = 0.01f * (touchDeltaMag - prevTouchDeltaMag);
                }
                    ZoomMapUsingTouchOrMouse(zoomFactor);
                    break;
            }
        }

        private void ZoomMapUsingTouchOrMouse(float zoomFactor)
        {
            var zoom = Mathf.Max(0.0f, Mathf.Min(_mapManager.Zoom + zoomFactor * _zoomSpeed, 21.0f));
            if (Math.Abs(zoom - _mapManager.Zoom) > 0.0f) _mapManager.UpdateMap(_mapManager.CenterLatitudeLongitude, zoom);
        }

        private void PanMapUsingKeyBoard(float xMove, float zMove)
        {
            if (Math.Abs(xMove) > 0.0f || Math.Abs(zMove) > 0.0f)
            {
                // Get the number of degrees in a tile at the current zoom level.
                // Divide it by the tile width in pixels ( 256 in our case)
                // to get degrees represented by each pixel.
                // Keyboard offset is in pixels, therefore multiply the factor with the offset to move the center.
                var factor = _panSpeed *
                             Conversions.GetTileScaleInDegrees((float)_mapManager.CenterLatitudeLongitude.x, _mapManager.AbsoluteZoom);

                var latitudeLongitude = new Vector2d(_mapManager.CenterLatitudeLongitude.x + zMove * factor * 2.0f,
                    _mapManager.CenterLatitudeLongitude.y + xMove * factor * 4.0f);

                _mapManager.UpdateMap(latitudeLongitude, _mapManager.Zoom);
            }
        }

        private void PanMapUsingTouchOrMouse()
        {
            if (_useDegreeMethod)
                UseDegreeConversion();
            else
                UseMeterConversion();
        }

        private void UseMeterConversion()
        {
            if (Input.GetMouseButtonUp(1))
            {
                var mousePosScreen = Input.mousePosition;
                //assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
                //http://answers.unity3d.com/answers/599100/view.html
                mousePosScreen.z = _referenceCamera.transform.localPosition.y;
                var pos = _referenceCamera.ScreenToWorldPoint(mousePosScreen);

                var latlongDelta = _mapManager.WorldToGeoPosition(pos);
                Debug.Log("Latitude: " + latlongDelta.x + " Longitude: " + latlongDelta.y);
            }

            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var mousePosScreen = Input.mousePosition;
                //assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
                //http://answers.unity3d.com/answers/599100/view.html
                mousePosScreen.z = _referenceCamera.transform.localPosition.y;
                _mousePosition = _referenceCamera.ScreenToWorldPoint(mousePosScreen);

                if (_shouldDrag == false)
                {
                    _shouldDrag = true;
                    _origin = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
                }
            }
            else
            {
                _shouldDrag = false;
            }

            if (_shouldDrag)
            {
                var changeFromPreviousPosition = _mousePositionPrevious - _mousePosition;
                if (Mathf.Abs(changeFromPreviousPosition.x) > 0.0f || Mathf.Abs(changeFromPreviousPosition.y) > 0.0f)
                {
                    _mousePositionPrevious = _mousePosition;
                    var offset = _origin - _mousePosition;

                    if (Mathf.Abs(offset.x) > 0.0f || Mathf.Abs(offset.z) > 0.0f)
                        if (null != _mapManager)
                        {
                            var factor = _panSpeed * Conversions.GetTileScaleInMeters(0, _mapManager.AbsoluteZoom) /
                                         _mapManager.UnityTileSize;
                            var latlongDelta = Conversions.MetersToLatLon(new Vector2d(offset.x * factor, offset.z * factor));
                            var newLatLong = _mapManager.CenterLatitudeLongitude + latlongDelta;

                            _mapManager.UpdateMap(newLatLong, _mapManager.Zoom);
                        }
                    _origin = _mousePosition;
                }
                else
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    _mousePositionPrevious = _mousePosition;
                    _origin = _mousePosition;
                }
            }
        }

        private void UseDegreeConversion()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var mousePosScreen = Input.mousePosition;
                //assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
                //http://answers.unity3d.com/answers/599100/view.html
                mousePosScreen.z = _referenceCamera.transform.localPosition.y;
                _mousePosition = _referenceCamera.ScreenToWorldPoint(mousePosScreen);

                if (_shouldDrag == false)
                {
                    _shouldDrag = true;
                    _origin = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
                }
            }
            else
            {
                _shouldDrag = false;
            }

            if (_shouldDrag)
            {
                var changeFromPreviousPosition = _mousePositionPrevious - _mousePosition;
                if (Mathf.Abs(changeFromPreviousPosition.x) > 0.0f || Mathf.Abs(changeFromPreviousPosition.y) > 0.0f)
                {
                    _mousePositionPrevious = _mousePosition;
                    var offset = _origin - _mousePosition;

                    if (Mathf.Abs(offset.x) > 0.0f || Mathf.Abs(offset.z) > 0.0f)
                        if (null != _mapManager)
                        {
                            // Get the number of degrees in a tile at the current zoom level.
                            // Divide it by the tile width in pixels ( 256 in our case)
                            // to get degrees represented by each pixel.
                            // Mouse offset is in pixels, therefore multiply the factor with the offset to move the center.
                            var factor = _panSpeed *
                                         Conversions.GetTileScaleInDegrees((float)_mapManager.CenterLatitudeLongitude.x,
                                             _mapManager.AbsoluteZoom) /
                                         _mapManager.UnityTileSize;

                            var latitudeLongitude = new Vector2d(_mapManager.CenterLatitudeLongitude.x + offset.z * factor,
                                _mapManager.CenterLatitudeLongitude.y + offset.x * factor);
                            _mapManager.UpdateMap(latitudeLongitude, _mapManager.Zoom);
                        }
                    _origin = _mousePosition;
                }
                else
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    _mousePositionPrevious = _mousePosition;
                    _origin = _mousePosition;
                }
            }
        }

        private Vector3 GetGroundPlaneHitPoint(Ray ray)
        {
            float distance;
            if (!_groundPlane.Raycast(ray, out distance)) return Vector3.zero;
            return ray.GetPoint(distance);
        }
    }
}