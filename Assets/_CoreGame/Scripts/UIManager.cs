using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Main components")]
    [SerializeField] private Button _GenerationButton;
    [SerializeField] private Button _SettingButton;

    [Header("Setting components")]
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Button _settingPanelSaveButton;
    [SerializeField] private Slider _modeSlider;
    [SerializeField] private TMP_Text _modeText;
    [SerializeField] private Slider _xSlider;
    [SerializeField] private TMP_Text _xText;
    [SerializeField] private Slider _ySlider;
    [SerializeField] private TMP_Text _yText;
    [SerializeField] private Toggle _isRandomColor;
    [SerializeField] private GameObject _ramdomColorMessage;
    [SerializeField] private Slider _offsetSlider;
    [SerializeField] private TMP_Text _offsetText;

    [Header("Gameplay components")]
    [SerializeField] private TMP_Text _moveText;
    [SerializeField] private TMP_Text _countText;

    private void Start()
    {
        _GenerationButton.onClick.AddListener(OnGenerationButtonClick);
        _SettingButton.onClick.AddListener(OnSettingButtonClick);
        _modeSlider.onValueChanged.AddListener(OnModeSliderValueChanged);
        _xSlider.onValueChanged.AddListener(OnXSliderValueChanged);
        _ySlider.onValueChanged.AddListener(OnYSliderValueChanged);
        _isRandomColor.onValueChanged.AddListener(OnIsRandomColorValueChanged);
        _offsetSlider.onValueChanged.AddListener(OnOffsetSliderValueChanged);
        _settingPanelSaveButton.onClick.AddListener(OnSettingPanelSaveButtonClick);
        GameplayManager.Instance.OnMoveValueChange += OnMoveValueChange;
        GameplayManager.Instance.OnCountValueChange += OnCountValueChange;
    }

    private void OnSettingPanelSaveButtonClick()
    {
        _settingPanel.SetActive(false);
    }

    private void OnModeSliderValueChanged(float value)
    {
        string mode = "Mode: ";
        mode += value switch
        {
            0 => "None",
            1 => "Fixed Border",
            2 => "Fixed Center",
            3 => "Fixed Three Rows",
            4 => "Fixed Three Columns",
            _ => "None",
        };
        _modeText.text = mode;
    }

    private void OnXSliderValueChanged(float value)
    {
        _xText.text = "X: " + value;
    }

    private void OnYSliderValueChanged(float value)
    {
        _yText.text = "Y: " + value;
    }

    private void OnIsRandomColorValueChanged(bool value)
    {
        _ramdomColorMessage.SetActive(value);
    }

    private void OnGenerationButtonClick()
    {
        GameplayManager.Instance.Generate(_isRandomColor.isOn, (int)_xSlider.value, (int)_ySlider.value, (int)_modeSlider.value, (int)_offsetSlider.value);
    }

    private void OnSettingButtonClick()
    {
        _settingPanel.SetActive(true);
    }

    private void OnOffsetSliderValueChanged(float value)
    {
        _offsetText.text = "Value: " + value;
    }

    private void OnMoveValueChange(int value)
    {
        _moveText.text = "Move: " + value;
    }

    private void OnCountValueChange(int value)
    {
        _countText.text = "Left: " + value;
    }
}

