﻿using Coimbra;
using Coimbra.Services;
using System;
using UnityEngine;


public class DaytimeSystem : Actor, IDaytimeService
{
    //[SerializeField] private float _passMinuteEverySeconds;
    //[SerializeField] private Gradient _astroLightFilterGradient;
    //[SerializeField] private AnimationCurve _astroLightIntensityCurve;
    //[SerializeField] private Light _astroLight;

    //private int _dayStartInMinutes;
    //private int _dayEndInMinutes;

    //public DateTime CurrentDayTime => _currentDayTime;
    //public float PassMinuteEverySeconds => _passMinuteEverySeconds;
    //public float HourDurationInRealSeconds => 60 * _passMinuteEverySeconds;

    //public event Action DayEndReached;
    //public event Action HourPassed;

    //protected override void OnInitialize()
    //{
    //    _currentDayTime = new DateTime(1970, 1, 1, _dayStartInHours, 0, 0);
    //    _dayStartInMinutes = _dayStartInHours * 60;
    //    _dayEndInMinutes = _dayEndInHours * 60;

    //    ServiceLocator.Set<IDaytimeService>(this);

    //    InvokeRepeating(nameof(UpdateTime), _passMinuteEverySeconds, _passMinuteEverySeconds);
    //}

    

    //private void UpdateTime()
    //{
    //    int lastRegisteredHour = _currentDayTime.Hour;

    //    _currentDayTime = _currentDayTime.AddMinutes(1);
        
    //    if (lastRegisteredHour < _currentDayTime.Hour)
    //    {
    //        HourPassed?.Invoke();
    //    }

    //    if (_currentDayTime.Hour == _dayEndInHours)
    //    {
    //        DayEndReached?.Invoke();
    //    }

    //    float currentMinutes = _currentDayTime.Hour * 60 + _currentDayTime.Minute;
    //    float timeOfDayNormalized = (currentMinutes - _dayStartInMinutes) / (_dayEndInMinutes - _dayStartInMinutes);

    //    _astroLight.intensity = _astroLightIntensityCurve.Evaluate(timeOfDayNormalized);
    //    _astroLight.color = _astroLightFilterGradient.Evaluate(timeOfDayNormalized);

    //    Debug.Log($"{GetType()} - H: {_currentDayTime.Hour}, M: {_currentDayTime.Minute}");
    //}

    //public void Dispose()
    //{
    //}
}

[DynamicService]
public interface IDaytimeService : IService { }
