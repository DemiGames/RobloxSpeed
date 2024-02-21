using TMPro;
using UnityEngine;

public class MainCanvasUpdate : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI currentSpeedText;
    [SerializeField]
    private TextMeshProUGUI passiveSpeedText;
    [SerializeField]
    private TextMeshProUGUI activeSpeedText;
    [SerializeField]
    private TextMeshProUGUI coinsText;


    private void OnEnable()
    {
        SpeedControl.SpeedIncreasesChanged += OnStatsValueChanged;
        SpeedControl.CurrentSpeedChanged += OnCurrentSpeedValueChanged;
        Wallet.CoinsChanged += OnCoinsValueChanged;
    }
    private void OnDisable()
    {
        SpeedControl.SpeedIncreasesChanged -= OnStatsValueChanged;
        SpeedControl.CurrentSpeedChanged -= OnCurrentSpeedValueChanged;
        Wallet.CoinsChanged -= OnCoinsValueChanged;
    }

    void OnCurrentSpeedValueChanged()
    {
        currentSpeedText.text = Bank.Instance.playerInfo.currentSpeed.ToString();
    }
   
    void OnStatsValueChanged() 
    {
        passiveSpeedText.text =
            (Bank.Instance.playerInfo.upgradePassiveSpeedIncrease + Bank.Instance.playerInfo.skinsPassiveSpeedIncrease)
            .ToString();
        activeSpeedText.text = 
            (Bank.Instance.playerInfo.upgradeActiveSpeedIncrease + Bank.Instance.playerInfo.skinsActiveSpeedIncrease)
            .ToString();
    }

    void OnCoinsValueChanged()
    {
        coinsText.text = Bank.Instance.playerInfo.coins.ToString();
    }
}
