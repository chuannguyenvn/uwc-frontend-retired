using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace UI.Sidebar.SidePanel
{
    public class SidePanelListElement : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _backgroundIconImage;
        [SerializeField] private Image _foregroundIconImage;
        [SerializeField] private TMP_Text _primaryText;
        [SerializeField] private TMP_Text _secondaryText;

        public void InitWorker(UserProfile worker)
        {
            gameObject.SetActive(true);
            _primaryText.text = worker.FirstName + " " + worker.LastName;
            _secondaryText.text = worker.Role.ToString();
        }

        public void InitMcp(Mcp mcp)
        {
            gameObject.SetActive(true);
            _primaryText.text = mcp.Address;
            _secondaryText.text = mcp.CurrentLoad + "/" + mcp.Capacity + "kgs";
        }

        public void InitVehicle(Vehicle vehicle)
        {
            gameObject.SetActive(true);
            _primaryText.text = vehicle.LicensePlate + " " + vehicle.VehicleType;
            _secondaryText.text = vehicle.CurrentLoad + "/" + vehicle.Capacity + "kgs";
        }

        public void InitMessage(Message message)
        {
            gameObject.SetActive(true);
            _primaryText.text = message.ReceiverAccount.LinkedProfile.FirstName + " " + message.ReceiverAccount.LinkedProfile.LastName;
            _secondaryText.text = message.TextContent;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}