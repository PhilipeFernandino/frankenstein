using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Systems.Inventory_System;
using UnityEngine;

namespace Assets.Project.Scripts.Systems.Crop_System
{
    [CreateAssetMenu(fileName = "Crop Data", menuName = "Crop System/Crop Data", order = 1)]
    public class CropData : ScriptableObject
    {
        [field: SerializeField] public List<CropPhaseData> _cropPhases = new();
    }

    [System.Serializable]
    public class CropPhaseData
    {
        [field: SerializeField] public Texture2D Texture { get; private set; }
        [field: SerializeField] public int PhaseHours { get; private set; }

        [field: Header("Harvest")]
        [field: SerializeField] public bool CanBeHarvested { get; private set; }
        [field: SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public int ItemAmount { get; private set; }
    }
}