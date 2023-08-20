using Managers;
using Map.Entity;
using Models;
using TMPro;
using UI.InformationPanel;
using UI.Sidebar.SidePanel.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel
{
    public class SidePanelListElement : MonoBehaviour
    {
        [SerializeField] private Button _button;
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

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
                InformationPanelController.Instance.ShowWorkerPanel(
                    MapEntityController.Instance.VehicleLocationResponse.Result[worker.Id - 10]));
        }

        public void InitMcp(Mcp mcp)
        {
            gameObject.SetActive(true);

            var fillPercentage = mcp.CurrentLoad / mcp.Capacity;
            _backgroundIconImage.color = fillPercentage switch
            {
                < 0.9f => VisualManager.Instance.McpNotFullColor,
                < 1f => VisualManager.Instance.McpNearlyFullColor,
                _ => VisualManager.Instance.McpFullColor
            };

            _foregroundIconImage.color = Color.white;

            _primaryText.text = mcp.Address;
            _secondaryText.text = mcp.CurrentLoad.ToString("F2") + "/" + mcp.Capacity.ToString("F2") + "kgs";

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => InformationPanelController.Instance.ShowMcpPanel(mcp));
        }

        public void InitVehicle(Vehicle vehicle)
        {
            gameObject.SetActive(true);
            _primaryText.text = vehicle.LicensePlate + " " + vehicle.VehicleType;
            _secondaryText.text = vehicle.CurrentLoad.ToString("F2") + "/" + vehicle.Capacity.ToString("F2") + "kgs";

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => InformationPanelController.Instance.ShowVehiclePanel(vehicle));
        }

        public void InitMessage(Message message)
        {
            gameObject.SetActive(true);

            var otherAccount = AuthenticationManager.Instance.UserId == message.SenderAccount.Id
                ? message.ReceiverAccount
                : message.SenderAccount;

            _primaryText.text = otherAccount.LinkedProfile.FirstName + " " + otherAccount.LinkedProfile.LastName;
            _secondaryText.text = message.TextContent;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => MessagingInboxPanel.Instance.Init(otherAccount.Id));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}