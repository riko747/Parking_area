using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ParkingLogic
{
    public class ParkingSlot : MonoBehaviour
    {
        [Inject] private IParking _parking;

        [SerializeField] private RectTransform _parkingSlotRectTransform;
        [SerializeField] private Button _parkingSlotButton;
        [SerializeField] private GameObject _entranceLabel;
        [SerializeField] private GameObject _mallEntranceLabel;
        [SerializeField] private GameObject _parkEntranceLabel;
        [SerializeField] private GameObject _parkingCarLabel;
        [SerializeField] private GameObject _yourCarLabel;

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public bool SlotIsNotAvailable { get; private set; }
        public bool SlotIsMallEntrance { get; private set; }
        public bool SlotIsParkEntrance {get; private set; }

        private void Start() => _parkingSlotButton.onClick.AddListener(CreateParkingCar);

        public void MoveParkingSlot(int x, int y)
        {
            PositionX = x;
            PositionY = y;
            transform.localPosition = new Vector2(x * -_parkingSlotRectTransform.sizeDelta.x, y * -_parkingSlotRectTransform.sizeDelta.y);
            ActivateLabels();
        }

        public void ActivateUserCarLabel() => _yourCarLabel.SetActive(true);

        private void ActivateLabels()
        {
            if (PositionX == 1 && PositionY == 1)
            {
                _entranceLabel.SetActive(true);
                SlotIsNotAvailable = true;
            }

            else if (PositionX == _parking.MaxSizeX && PositionY == _parking.MaxSizeY / 2)
            { 
                _mallEntranceLabel.SetActive(true);
                SlotIsMallEntrance = true;
            }

            else if (PositionX == _parking.MaxSizeX / 2 + 1 && PositionY == _parking.MaxSizeY)
            {
                _parkEntranceLabel.SetActive(true);
                SlotIsParkEntrance = true;
            }
        }

        private void CreateParkingCar()
        {
            if (SlotIsNotAvailable) return;

            _parkingCarLabel.SetActive(true);
            SlotIsNotAvailable = true;
        }

        public void RemoveYourCar() => _yourCarLabel.SetActive(false);

        private void OnDestroy() => _parkingSlotButton.onClick.RemoveListener(CreateParkingCar);
    }
}
