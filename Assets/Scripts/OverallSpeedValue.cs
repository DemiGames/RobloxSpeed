using TMPro;
using UnityEngine;

public class OverallSpeedValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overallSpeedText;


    private void OnEnable()
    {
        SpeedControl.CurrentSpeedChanged += OnCurrentSpeedChange;
    }
    private void OnDisable()
    {
        SpeedControl.CurrentSpeedChanged -= OnCurrentSpeedChange;
    }
    void OnCurrentSpeedChange()
    {
        overallSpeedText.text = Bank.Instance.playerInfo.overallSpeed.ToString();
        YandexSDK.SetNewLeaderboardValue(Bank.Instance.playerInfo.overallSpeed);
    }
}
