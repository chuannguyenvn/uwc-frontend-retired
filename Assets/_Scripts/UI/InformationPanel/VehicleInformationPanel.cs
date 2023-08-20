using DG.Tweening;
using Map.Entity;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class VehicleInformationPanel : InformationPanel
    {
        [SerializeField] private TMP_Text _vehicleTypeText;
        [SerializeField] private TMP_Text _licensePlateText;
        [SerializeField] private TMP_Text _modelText;
        [SerializeField] private TMP_Text _capacityText;
        [SerializeField] private TMP_Text _averageSpeedText;

        [SerializeField] private RectTransform _fillLevelBarRectTransform;
        [SerializeField] private Image _fillLevelBarImage;
        private float _initialFillLevelBarWidth;

        protected override void Awake()
        {
            base.Awake();
            _initialFillLevelBarWidth = _fillLevelBarRectTransform.rect.width;
        }

        public void Show(Vehicle vehicle)
        {
            _vehicleTypeText.text = VisualManager.Instance.GetVehicleTypeString(vehicle.VehicleType);
            _licensePlateText.text = vehicle.LicensePlate;
            _modelText.text = "Model: " + vehicle.Model;
            _capacityText.text = "Capacity: " + vehicle.Capacity.ToString("F0") + "kgs";
            _averageSpeedText.text = "Average speed :" + vehicle.AverageSpeed.ToString("F0") + "km/h";

            var fillPercentage = (float)(vehicle.CurrentLoad / vehicle.Capacity);
            var fillPercentageText = (fillPercentage * 100).ToString("F0");

            _fillLevelBarRectTransform.sizeDelta = _fillLevelBarRectTransform.sizeDelta.WithX(_initialFillLevelBarWidth * fillPercentage);
            _fillLevelBarImage.color = VisualManager.Instance.GetFillLevelColor(fillPercentage);

            if (!_isShowing)
            {
                _movementTween?.Kill();
                _movementTween = _rectTransform.DOAnchorPosX(0, 0.15f).SetEase(Ease.OutCubic);
            }

            _isShowing = true;
            
            DriverMapEntity.CurrentlySelectedDriverMapEntity = MapEntityController.Instance.DriverMapEntitiesById[vehicle.Id];
        }

        public override void Hide()
        {
            base.Hide();
            DriverMapEntity.CurrentlySelectedDriverMapEntity = null;
        }
        
        public override void HideInstant()
        {
            base.HideInstant();
            DriverMapEntity.CurrentlySelectedDriverMapEntity = null;
        }
    }
}