using UnityEngine;

namespace Systems.Grid_System
{
    [CreateAssetMenu(fileName = "Tile Data", menuName = "Grid System/Inventory Item", order = 1)]
    public class TileData : ScriptableObject
    {
        public TilePropeties Properties { get; private set; }

        public bool IsDiggable => Properties.HasFlag(TilePropeties.Diggable);
        public bool IsPlantable => Properties.HasFlag(TilePropeties.Plantable);
        public bool IsConstructable => Properties.HasFlag(TilePropeties.Constructable);

        [System.Flags]
        public enum TilePropeties
        {
            None = 0,
            Constructable = 1,
            Plantable = 2,
            Diggable = 4, 
        }
    }

    public interface ITileData
    {

    }

}