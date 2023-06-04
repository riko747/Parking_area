using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ToolTip
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _sliderValue;

        private void Start()
        {
            _slider.wholeNumbers = true;
            _slider.minValue = 2;
            _slider.maxValue = 8;
            _slider.value = 2;
            _sliderValue.text = _slider.value.ToString();
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            var intValue = Mathf.RoundToInt(value);
            if (intValue % 2 != 0)
                intValue -= 1;
            _slider.value = intValue;
            _sliderValue.text = _slider.value.ToString();
        }
    }
}
