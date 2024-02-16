using Coimbra;
using Coimbra.Services.Events;
using Interaction;
using NaughtyAttributes;
using System;
using Systems.Item_System;
using Systems.Time_System;
using UnityEngine;
using Utils;

namespace Systems.Crop_System
{
    public class Crop : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer _cropSpriteRenderer;
        [SerializeField] private CropData _cropData;

        private int _currentMinutesInThisPhase = 0;

        private int _phaseTotalMinutes;
        private int _phaseCurrentIndex;

        private EventHandle _minutePassedEventHandler;
        private IGroundItemFactoryService _groundItemFactory;

        public CropPhaseData CurrentCropPhaseData => _cropData.CropPhases[_phaseCurrentIndex];

        [Button]
        public void Interact()
        {
            if (CurrentCropPhaseData.CanBeHarvested)
            {
                Harvest();
            }
        }

        private void Harvest()
        {
            var item = _groundItemFactory.Create(CurrentCropPhaseData.ItemData, CurrentCropPhaseData.ItemAmount);
            item.transform.position = transform.position;
            gameObject.Dispose(true);
        }

        private void Start()
        {
            _minutePassedEventHandler = MinutePassed.AddListener(MinutePassedEventHandler);

            _groundItemFactory = ServiceLocatorUtilities.GetServiceAssert<IGroundItemFactoryService>();
            
            SetupPhase(0);
        }

        private void MinutePassedEventHandler(ref EventContext context, in MinutePassed e)
        {
            _currentMinutesInThisPhase += e.MinutesPassed;

            if (_currentMinutesInThisPhase > _phaseTotalMinutes)
            {
                NextPhase();
            }
        }

        private void NextPhase()
        {
            if (_phaseCurrentIndex >= _cropData.CropPhases.Count - 1)
            {
                _minutePassedEventHandler.Service.RemoveListener(_minutePassedEventHandler);
            }
            else
            {
                SetupPhase(_phaseCurrentIndex + 1);
            }
        }

        private void SetupPhase(int phaseIndex)
        {
            if (phaseIndex < 0 || phaseIndex >= _cropData.CropPhases.Count)
            {
                Debug.LogError($"{GetType()} - Trying to setup crop phase with invalid index: {phaseIndex}");
                return;
            }

            var cropPhaseData = _cropData.CropPhases[phaseIndex];
            _cropSpriteRenderer.sprite = cropPhaseData.Sprite;
            _phaseTotalMinutes = cropPhaseData.PhaseHours * 60;
            _phaseCurrentIndex = phaseIndex;
            _currentMinutesInThisPhase = 0;
        }
    }
}