using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ParkingLogic
{
    interface IParking
    {
        void GenerateParking(int xSize, int ySize);
        int MaxSizeX { get; set; }
        int MaxSizeY { get; set; }
        List<ParkingSlot> ParkingSlots { get; set; }
    }
    public class Parking : MonoBehaviour, IParking
    {
        [SerializeField] private ParkingSlot _parkingSlotPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private IInstantiator _instantiator;

        public int MaxSizeX { get; set; }
        public int MaxSizeY { get; set; }
        public List<ParkingSlot> ParkingSlots { get; set; } = new List<ParkingSlot>();

        [Inject]
        public void Initialize(IInstantiator instantiator) => _instantiator = instantiator;

        public void GenerateParking(int xSize, int ySize)
        {
            if (ParkingSlots.Count != 0)
            {
                foreach (var parkingSlot in ParkingSlots)
                    Destroy(parkingSlot.gameObject);
                ParkingSlots.Clear();
            }

            MaxSizeX = xSize;
            MaxSizeY = ySize;

            _gridLayoutGroup.constraintCount = xSize;
            for (var i = 1; i < xSize + 1; i++)
            {
                for (var j = 1; j < ySize + 1; j++)
                {
                    var currentParkingSlot = _instantiator.InstantiatePrefab(_parkingSlotPrefab, transform).GetComponent<ParkingSlot>();
                    ParkingSlots.Add(currentParkingSlot);
                    currentParkingSlot.MoveParkingSlot(i, j);
                }
            }
        }
    }
}
