using Commons;
using DG.Tweening;
using Map.Entity;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class WorkerInformationPanel : InformationPanel
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _roleText;
        [SerializeField] private Button _assignTaskButton;
        [SerializeField] private Button _messageTaskButton;

        public void Show(VehicleMovementData data)
        {
            _nameText.text = data.DriverProfile.FirstName + " " + data.DriverProfile.LastName;
            _roleText.text = data.DriverProfile.Role.ToString();

            if (!_isShowing)
            {
                _movementTween?.Kill();
                _movementTween = _rectTransform.DOAnchorPosX(0, 0.15f).SetEase(Ease.OutCubic);
            }

            _isShowing = true;
            
            DriverMapEntity.CurrentlySelectedDriverMapEntity = MapEntityController.Instance.DriverMapEntitiesById[data.DriverProfile.Id];
        }
    }
}