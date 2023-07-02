using UnityEngine;

public static class Mathfs
{
    public static bool Approximately(Vector3 a, Vector3 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) &&
               Mathf.Approximately(a.z, b.z);
    }
    
    public static bool Approximately(Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }

    public static bool Approximately(double a, int b)
    {
        return Mathf.Approximately((float)a, b);
    }

    public static Vector3 Sign(Vector3 vector3)
    {
        return new Vector3(Mathf.Sign(vector3.x), Mathf.Sign(vector3.y), Mathf.Sign(vector3.z));
    }

    public static Vector3 Round(Vector3 vector3)
    {
        return new Vector3(Mathf.Round(vector3.x), Mathf.Round(vector3.y), Mathf.Round(vector3.z));
    }
    
    public static bool InRange(this int n, int a, int b)
    {
        return n >= a && n <= b || n >= b && n <= a;
    }
}