using System;
using System.Collections.Generic;
using System.Linq;
using Commons;
using Commons.Communications.Task;
using DG.Tweening;
using Https;
using Managers;
using Map.Entity;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class WorkerInformationPanel : InformationPanel
    {
        public static bool IsAssigning;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _roleText;
        [SerializeField] private TMP_Text _assignTaskButtonText;
        [SerializeField] private Button _assignTaskButton;
        [SerializeField] private TMP_Text _messageTaskButtonText;
        [SerializeField] private Button _messageTaskButton;
        private int _workerId;

        public void Show(VehicleMovementData data)
        {
            _workerId = data.DriverProfile.Id;

            _nameText.text = data.DriverProfile.FirstName + " " + data.DriverProfile.LastName;
            _roleText.text = data.DriverProfile.Role.ToString();

            if (!_isShowing)
            {
                _movementTween?.Kill();
                _movementTween = _rectTransform.DOAnchorPosX(0, 0.15f).SetEase(Ease.OutCubic);
            }

            _isShowing = true;

            DriverMapEntity.CurrentlySelectedDriverMapEntity =
                MapEntityController.Instance.DriverMapEntitiesById[data.DriverProfile.Id - 10];
            _assignTaskButton.onClick.AddListener(ToggleAssigningMode);
            _messageTaskButton.onClick.AddListener(MessageButtonClickHandler);
        }

        private void ToggleAssigningMode()
        {
            IsAssigning = true;

            if (IsAssigning)
            {
                _assignTaskButtonText.text = "Confirm";
                _messageTaskButtonText.text = "Cancel";
                _assignTaskButton.onClick.AddListener(RequestAddTask);
            }
            else
            {
                _assignTaskButtonText.text = "Assign Task";
                _messageTaskButtonText.text = "Message";
                _messageTaskButton.onClick.AddListener(MessageButtonClickHandler);
            }
        }

        private void MessageButtonClickHandler()
        {
        }

        private void RequestAddTask()
        {
            ToggleAssigningMode();
            Debug.Log(AuthenticationManager.Instance.UserId);
            StartCoroutine(HttpClient.SendRequest(Endpoints.Task.ADD,
                HttpClient.RequestType.POST,
                success =>
                {
                    if (success) Debug.Log("Added tasks.");
                },
                "",
                new AddTaskRequest
                {
                    Date = DateTime.UtcNow,
                    SupervisorId = AuthenticationManager.Instance.UserId,
                    WorkerId = _workerId,
                    McpIds = McpMapEntity.SelectedMcps.Select(entity => entity.Id).ToList()
                }));
        }
    }
}