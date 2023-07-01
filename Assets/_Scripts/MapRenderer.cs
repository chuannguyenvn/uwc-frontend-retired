using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapRenderer : MonoBehaviour
{
    public enum MapStyle
    {
        Roadmap,
        Satellite,
        Hybrid,
        Terrain
    }

    public enum Resolution
    {
        Low = 1,
        High = 2
    }

    public float Latitude = -33.85660618894087f;
    public float Longitude = 151.21500701957325f;
    public int Zoom = 12;

    public Resolution mapResolution = Resolution.Low;

    public MapStyle mapMapStyle = MapStyle.Roadmap;
    private MapStyle _mapMapStyleLast = MapStyle.Roadmap;

    private float _lastLatitude = -33.85660618894087f;
    private float _lastLongitude = 151.21500701957325f;
    private int mapHeight = 640;
    private bool _mapIsLoading;

    public bool MapIsLoading => _mapIsLoading;

    private Resolution _lastResolution = Resolution.Low;
    private int _mapWidth = 640;
    private Rect _rect;
    private bool _updateMap = true;
    private int _lastZoom = 12;

    private void Start()
    {
        StartCoroutine(RequestMapTexture());
        _rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        _mapWidth = (int)Math.Round(_rect.width);
        mapHeight = (int)Math.Round(_rect.height);
    }

    private void Update()
    {
        if (_updateMap && (!Mathf.Approximately(_lastLatitude, Latitude) || !Mathf.Approximately(_lastLongitude, Longitude) ||
                           _lastZoom != Zoom || _lastResolution != mapResolution || _mapMapStyleLast != mapMapStyle))
        {
            _rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
            _mapWidth = (int)Math.Round(_rect.width);
            mapHeight = (int)Math.Round(_rect.height);
            StartCoroutine(RequestMapTexture());
            _updateMap = false;
        }
    }

    private IEnumerator RequestMapTexture()
    {
        var url = "https://maps.googleapis.com/maps/api/staticmap?center=" + Latitude + "," + Longitude + "&zoom=" + Zoom + "&size=" +
                  _mapWidth + "x" + mapHeight + "&scale=" + 5 + "&maptype=" + mapMapStyle + "&key=" +
                  Constants.Credentials.GOOGLE_MAP_API_KEY;
        _mapIsLoading = true;
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            _mapIsLoading = false;
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            _lastLatitude = Latitude;
            _lastLongitude = Longitude;
            _lastZoom = Zoom;
            _lastResolution = mapResolution;
            _mapMapStyleLast = mapMapStyle;
            _updateMap = true;
        }
    }
}