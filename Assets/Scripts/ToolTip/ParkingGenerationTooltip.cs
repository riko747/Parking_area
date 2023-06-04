using Assets.Scripts.ParkingLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ToolTip
{
    public class ParkingGenerationTooltip : MonoBehaviour
    {
        [Inject] private IParking _parking;

        [SerializeField] private TextMeshProUGUI _xSliderValue;
        [SerializeField] private TextMeshProUGUI _ySliderValue;
        [SerializeField] private Button _generateButton;

        private void Start() => _generateButton.onClick.AddListener(PrepareToGenerateParkingArea);

        private void PrepareToGenerateParkingArea()
        {
            int.TryParse(_xSliderValue.text, out var parsedXSize);
            int.TryParse(_ySliderValue.text, out var parsedYSize);
            _parking.GenerateParking(parsedXSize, parsedYSize);
        }

        private void OnDestroy() => _generateButton.onClick.RemoveListener(PrepareToGenerateParkingArea);
    }
}
