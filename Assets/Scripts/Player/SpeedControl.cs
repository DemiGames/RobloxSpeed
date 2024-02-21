using ECM.Components;
using ECM.Controllers;
using System;
using TMPro;
using UnityEngine;

public class SpeedControl : MonoBehaviour
{
    [Header("SPEED")]
    [SerializeField]
    private bool isSpeedIncrease;
    [SerializeField]
    private uint minLevelSpeed;
    [SerializeField]
    private int levelSpeedKoeficient;

    private float speedIncreaseTimer = 0f;
    private float speedIncreaseInterval = 1f;

    [SerializeField]
    private BaseCharacterController characterController;
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController;

    uint upgradesPassiveSpeedIncrease;
    uint upgradesActiveSpeedIncrease;
    uint skinsPassiveSpeedIncrease;
    uint skinsActiveSpeedIncrease;
    uint passiveSpeedIncrease;
    uint activeSpeedIncrease;
    uint currentSpeed;

    public static event Action SpeedIncreasesChanged;
    public static event Action CurrentSpeedChanged;
    [SerializeField]
    private SkinCharacteristics[] skinsCharacteristics;

    float saveTimer;
    float saveInterval = 3;
    private void Awake()
    {
        characterController = FindObjectOfType<BaseCharacterController>();
        playerAnimatorController = FindObjectOfType<PlayerAnimatorController>();
        
    }
    private void OnEnable()
    {
        CourseFinish.CourseFinished += ResetSpeedToMinLevel;
        SkinCharacteristics.CharacteristicsChanged += OnSkinsCharacteristicsChanged;
    }
    private void OnDisable()
    {
        CourseFinish.CourseFinished -= ResetSpeedToMinLevel;
        SkinCharacteristics.CharacteristicsChanged -= OnSkinsCharacteristicsChanged;
    }

    void Start()
    {
        Initialization();
        ResetTimer();
        saveTimer = saveInterval;
    }
    void Initialization()
    {
        currentSpeed = Bank.Instance.playerInfo.currentSpeed;
        currentSpeed = (uint)Mathf.Max(currentSpeed, minLevelSpeed);
        skinsPassiveSpeedIncrease = Bank.Instance.playerInfo.skinsPassiveSpeedIncrease;
        skinsActiveSpeedIncrease = Bank.Instance.playerInfo.skinsActiveSpeedIncrease;
        upgradesPassiveSpeedIncrease = Bank.Instance.playerInfo.upgradePassiveSpeedIncrease;   
        upgradesActiveSpeedIncrease = Bank.Instance.playerInfo.upgradeActiveSpeedIncrease;

        passiveSpeedIncrease = upgradesPassiveSpeedIncrease + skinsPassiveSpeedIncrease;
        activeSpeedIncrease = upgradesActiveSpeedIncrease + skinsActiveSpeedIncrease;
        ChangePlayerCurrentSpeed();
        SpeedIncreasesChanged?.Invoke();
    }
  
    void Update()
    {
        SaveTimer();
        if (isTimeToIncrease())
        {
            PassiveIncreaseCurrentSpeed();
            ResetTimer();
        }
    }
    //По ивенту
    void OnSkinsCharacteristicsChanged()
    {
        skinsPassiveSpeedIncrease = 0;
        skinsActiveSpeedIncrease = 0;
        foreach (var characteristic in skinsCharacteristics)
        {
            skinsPassiveSpeedIncrease += characteristic.GetPassiveStats();
            skinsActiveSpeedIncrease += characteristic.GetActiveStats();
        }

        Bank.Instance.playerInfo.skinsActiveSpeedIncrease = skinsActiveSpeedIncrease;
        Bank.Instance.playerInfo.skinsPassiveSpeedIncrease = skinsPassiveSpeedIncrease;
        YandexSDK.Save();
        ChangeSpeedIncreases();
    }

    void PassiveIncreaseCurrentSpeed()
    {
        currentSpeed += passiveSpeedIncrease;
        Bank.Instance.playerInfo.overallSpeed += passiveSpeedIncrease;
        ChangePlayerCurrentSpeed();
    }
    public void ActiveIncreaseCurrentSpeed(int streakMultiply)
    {
        currentSpeed += (uint)((activeSpeedIncrease) * streakMultiply);
        Bank.Instance.playerInfo.overallSpeed += (uint)((activeSpeedIncrease) * streakMultiply);
        ChangePlayerCurrentSpeed();
    }

    public void ChangeUpgradesPassiveSpeedIncrease(int upgradesDiff)
    {
        upgradesPassiveSpeedIncrease += (uint)upgradesDiff;
        Bank.Instance.playerInfo.upgradePassiveSpeedIncrease = upgradesPassiveSpeedIncrease;
        YandexSDK.Save();
        ChangeSpeedIncreases();
    }
    public void ChangeUpgradesActiveSpeedIncrease(int upgradesDiff)
    {
        upgradesActiveSpeedIncrease += (uint)upgradesDiff;
        Bank.Instance.playerInfo.upgradeActiveSpeedIncrease = upgradesActiveSpeedIncrease;
        YandexSDK.Save();
        ChangeSpeedIncreases();
    }
    void ChangeSpeedIncreases()
    {       
        passiveSpeedIncrease = skinsPassiveSpeedIncrease + upgradesPassiveSpeedIncrease;
        activeSpeedIncrease = skinsActiveSpeedIncrease + upgradesActiveSpeedIncrease;
        SpeedIncreasesChanged?.Invoke();
    }

    void ChangePlayerCurrentSpeed()
    {
        Bank.Instance.playerInfo.currentSpeed = currentSpeed;
        
        characterController.speed = currentSpeed / levelSpeedKoeficient;
        characterController.acceleration =  characterController.speed * 1.5f;
        characterController.deceleration = characterController.speed * 2f;

        UpdateSpeedAnimation();
        CurrentSpeedChanged?.Invoke();
    }

    void UpdateSpeedAnimation()
    {
        float speedAnimationMultiplier = (float)currentSpeed / minLevelSpeed / 5;
        speedAnimationMultiplier = Mathf.Clamp(speedAnimationMultiplier, 1f, 3f);
        playerAnimatorController.SetSpeedMultiplier(speedAnimationMultiplier);
    }
    public void ResetSpeedToMinLevel(int diff)
    {
        currentSpeed = minLevelSpeed;
        ChangePlayerCurrentSpeed();
    }

    bool isTimeToIncrease()
    {
        speedIncreaseTimer -= Time.deltaTime;
        return speedIncreaseTimer <= 0;
    }
    void ResetTimer()
    {
        speedIncreaseTimer = speedIncreaseInterval;
    }

    void SaveTimer()
    {
        saveTimer -= Time.deltaTime;
        if (saveTimer <= 0)
        {
            YandexSDK.Save();
            saveTimer = saveInterval;
        }
    }
    
}
