
using UnityEngine;

namespace Systems.Time_System
{
    [CreateAssetMenu(fileName = "Time Rate", menuName = "Time System/Time Rate", order = 1)]
    public class TimeRateData : ScriptableObject
    {
        [field: SerializeField] public int DaysPerMonth;

        [field: SerializeField] public int MonthsPerYear;

        [field: SerializeField] public int MinutesPerSecond; 
    }
}