using TMPro;
using UnityEngine;

public class UpgradesStatsShow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI passiveUpgradeStatsText;
    [SerializeField]
    private TextMeshProUGUI activeUpgradeStatsText;

    string secInterText, clickInterText;
    private void OnEnable()
    {
        SetInternationalText();
        SpeedControl.SpeedIncreasesChanged += OnStatsValueChanged;
    }
    private void OnDisable()
    {
        SpeedControl.SpeedIncreasesChanged -= OnStatsValueChanged;
    }
    public void UpdateUpgradeText(TextMeshProUGUI _textField, string _text)
    {
        _textField.text = _text;
    }
    void OnStatsValueChanged()
    {
        string _text = $"+{Bank.Instance.playerInfo.upgradePassiveSpeedIncrease}/{secInterText}";
        UpdateUpgradeText(passiveUpgradeStatsText, _text);
        _text = $"+{Bank.Instance.playerInfo.upgradeActiveSpeedIncrease}/{clickInterText}";
        UpdateUpgradeText(activeUpgradeStatsText, _text);
    }

    void SetInternationalText()
    {
        if (Language.Instance.languageName == LanguageName.Rus)
        {
            secInterText = "сек";
            clickInterText = "клик";
        }
        else
        {
            secInterText = "sec";
            clickInterText = "click";
        }
    }
}
