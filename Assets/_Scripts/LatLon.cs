using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatLon
{
    private static Vector2 UNITY_NORTH_WEST_POSITION = new Vector2(-10, 10);
    private static Vector2 UNITY_SOUTH_EAST_POSITION = new Vector2(10, -10);
    private static Vector2 GEOGRAPHIC_NORTH_WEST_POSITION = new Vector2(10.779947f, 106.651529f);
    private static Vector2 GEOGRAPHIC_SOUTH_EAST_POSITION = new Vector2(10.761235f, 106.673746f);

    //class that converts latitude / longitude to Unity position and the reverse
    //Got the formula from here
    //https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio

    static LatLon()
    {
        //check if this zone covers the antimeridian (where 180 and -180 degress longitude meet)
        if (GEOGRAPHIC_SOUTH_EAST_POSITION.y < GEOGRAPHIC_NORTH_WEST_POSITION.y)
        {
            //Add 360 to any negative longitude positions so that longitude values are lower the further west
            GEOGRAPHIC_SOUTH_EAST_POSITION = new Vector2(GEOGRAPHIC_SOUTH_EAST_POSITION.x, GEOGRAPHIC_SOUTH_EAST_POSITION.y + 360f);
        }
    }
    
    //convert a coordinate from one set of ranges to another set of ranges
    private static float convertCoordinate(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float returnValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return returnValue;
    }

    //A LatLon Vector2 includes Latitude as the x value and Longitude as the y value
    //A Unity world coordinate has x as the west/east (longitude) and z as the north/sounth (latitude)

    //This method takes a LatLon Vector2 and translates it into this zone's game world coordinates
    //It does this by taking two points, a Noth West point and South East point in both LatLon and Unity world space positions to do the translation
    public static Vector3 GetUnityPosition(Vector2 latLonPosition)
    {
        if (GEOGRAPHIC_SOUTH_EAST_POSITION.y < GEOGRAPHIC_NORTH_WEST_POSITION.y)
        {
            if (latLonPosition.y < 0f)
            {
                latLonPosition = new Vector2(latLonPosition.x, latLonPosition.y + 360f);
            }
        }
        float newUnityLat = convertCoordinate(latLonPosition.x,
            GEOGRAPHIC_SOUTH_EAST_POSITION.x,
            GEOGRAPHIC_NORTH_WEST_POSITION.x,
            UNITY_SOUTH_EAST_POSITION.z,
            UNITY_NORTH_WEST_POSITION.z);
        float newUnityLon = convertCoordinate(latLonPosition.y,
            GEOGRAPHIC_SOUTH_EAST_POSITION.y,
            GEOGRAPHIC_NORTH_WEST_POSITION.y,
            UNITY_SOUTH_EAST_POSITION.x,
            UNITY_NORTH_WEST_POSITION.x);
        Vector3 unityWorldPosition = new Vector3(newUnityLon, 200f, newUnityLat);
        return unityWorldPosition;
    }

    public static Vector2 GetLatLonPosition(Vector3 unityPosition)
    {
        bool antimeridian = false;
        //check if this zone covers the antimeridian (where 180 and -180 degress longitude meet)
        if (GEOGRAPHIC_SOUTH_EAST_POSITION.y < GEOGRAPHIC_NORTH_WEST_POSITION.y)
        {
            antimeridian = true;
            //Add 360 to any negative longitude positions so that longitude values are lower the further west
            GEOGRAPHIC_SOUTH_EAST_POSITION = new Vector2(GEOGRAPHIC_SOUTH_EAST_POSITION.x, GEOGRAPHIC_SOUTH_EAST_POSITION.y + 360f);
        }
        float newlat = convertCoordinate(unityPosition.z,
            UNITY_SOUTH_EAST_POSITION.z,
            UNITY_NORTH_WEST_POSITION.z,
            GEOGRAPHIC_SOUTH_EAST_POSITION.x,
            GEOGRAPHIC_NORTH_WEST_POSITION.x);
        float newlon = convertCoordinate(unityPosition.x,
            UNITY_SOUTH_EAST_POSITION.x,
            UNITY_NORTH_WEST_POSITION.x,
            GEOGRAPHIC_SOUTH_EAST_POSITION.y,
            GEOGRAPHIC_NORTH_WEST_POSITION.y);
        if (antimeridian)
        {
            if (newlon > 180f)
            {
                newlon = newlon - 360f;
            }
        }
        Vector2 latLonPosition = new Vector2(newlat, newlon);
        return latLonPosition;
    }
}