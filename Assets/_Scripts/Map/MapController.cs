using Mapbox.Unity.Map;
using UnityEngine;

namespace Map
{
    public class MapController : Singleton<MapController>
    {
        [SerializeField] private AbstractMap _abstractMap;
    }
}