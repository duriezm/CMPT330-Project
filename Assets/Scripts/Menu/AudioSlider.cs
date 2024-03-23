using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Slider _slider;

    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString();
        });
    }
}
