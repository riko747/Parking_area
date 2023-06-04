using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ParkingLogic
{
    public class UserCarParking : MonoBehaviour
    {
        [Inject] private IParking _parking;

        [SerializeField] private Button _parkYourCarButton;
        [SerializeField] private Toggle _mallToggle;
        [SerializeField] private Toggle _parkToggle;

        private void Start() => _parkYourCarButton.onClick.AddListener(ParkUserCar);

        private void ParkUserCar()
        {
            foreach (var parkingSlot in _parking.ParkingSlots)
                parkingSlot.RemoveYourCar();

            if (_mallToggle.isOn)
                ParkUserCarToMall();
            else if (_parkToggle.isOn)
                ParkUserCarToPark();
        }

        private void ParkUserCarToMall()
        {
            var availableSlots = _parking.ParkingSlots.Where(slot => !slot.SlotIsNotAvailable);
            var mallEntrance = _parking.ParkingSlots.First(slot => slot.SlotIsMallEntrance);

            var closestSlots = availableSlots
                .OrderBy(slot => Vector2.Distance(slot.transform.localPosition, mallEntrance.transform.localPosition))
                .ToList();

            var mostClosestSlots = closestSlots
                .Where(slot => slot.PositionX == _parking.MaxSizeX)
                .ToList();

            if (mostClosestSlots.Count > 0)
                mostClosestSlots.First().ActivateUserCarLabel();
            else
                closestSlots.First().ActivateUserCarLabel();
        }

        private void ParkUserCarToPark()
        {
            var availableSlots = _parking.ParkingSlots.Where(slot => !slot.SlotIsNotAvailable);
            var parkEntrance = _parking.ParkingSlots.First(slot => slot.SlotIsParkEntrance);

            var closestSlots = availableSlots
                .OrderBy(slot => Vector2.Distance(slot.transform.localPosition, parkEntrance.transform.localPosition))
                .ToList();

            var mostClosestSlots = closestSlots
                .Where(slot => slot.PositionY == _parking.MaxSizeY)
                .ToList();

            if (mostClosestSlots.Count > 0)
                mostClosestSlots.First().ActivateUserCarLabel();
            else
                closestSlots.First().ActivateUserCarLabel();
        }

        private void OnDestroy() => _parkYourCarButton.onClick.RemoveListener(ParkUserCar);
    }
}
