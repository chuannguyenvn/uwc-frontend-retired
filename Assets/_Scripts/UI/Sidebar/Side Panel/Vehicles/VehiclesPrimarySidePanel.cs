using System.Collections;
using System.Collections.Generic;
using Https;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Vehicles
{
    public class VehiclesPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void Awake()
        {
            SidePanelType = SidePanelType.Vehicles;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            yield return HttpClient.SendRequest<List<Vehicle>>(Endpoints.Vehicle.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitList(result);
                },
                "");
        }

        private void InitList(List<Vehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                var element = SidePanelListElementPool.Instance.GetElement(_scrollRect.content);
                element.InitVehicle(vehicle);
            }
        }
    }
}