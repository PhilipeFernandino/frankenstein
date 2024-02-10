
using UnityEngine;

namespace Systems.Time_System
{
    public class DateTimeSystem : MonoBehaviour
    {
        [SerializeField] private TimeRateData _timeRateData;

        private float _seconds;
        private int _minutes;
        private int _hours;

        private int _days;
        private int _months;
        private int _years;

        public float Seconds
        {
            get => _seconds;
            set {
                _seconds = value;
                if (_seconds >= 60)
                {
                    Minutes += (int) _seconds / 60;
                    _seconds %= 60;
                }
            }
        }

        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                if (_minutes >= 60)
                {
                    Hours += _minutes / 60;
                    _minutes %= 60;
                }
            }
        }

        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                if (_hours >= 24)
                {
                    Days += _hours / 24;
                    _hours %= 24;
                }
            }
        }

        public int Days
        {
            get => _days;
            set
            {
                _days = value;
                
                if (_days >= _timeRateData.DaysPerMonth)
                {
                    Months += _days / _timeRateData.DaysPerMonth;
                    _days %= _timeRateData.DaysPerMonth;
                }
            }
        }

        public int Months
        {
            get => _months;
            set
            {
                _months = value;
                if (_months > _timeRateData.MonthsPerYear)
                {
                    Years += _months / _timeRateData.MonthsPerYear;
                    _months %= _timeRateData.MonthsPerYear;
                }
            }
        }

        public int Years
        {
            get => _years;
            set
            {
                _years = value;
            }
        }

        private void Update()
        {
            Seconds += Time.deltaTime * (60 * _timeRateData.MinutesPerSecond);
        }
    }
}