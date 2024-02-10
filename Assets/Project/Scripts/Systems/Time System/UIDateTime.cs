using System.Collections;
using Systems.Time_System;
using TMPro;
using UnityEngine;

namespace Assets.Project.Scripts.Systems.Time_System
{
    public class UIDateTime : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        private DateTimeSystem _dateTimeSystem;

        private void Awake()
        {
            _dateTimeSystem = FindObjectOfType<DateTimeSystem>();
        }

        public void Update()
        {
            _timeText.text = $"{_dateTimeSystem.Hours:00}:{_dateTimeSystem.Minutes:00}";
        }
    }
}