using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public float musicVolume = 0.5f;                //++
    public float effectsVolume = 0.5f;              //++
    public float sensivityValue = 1f;               //++

    public uint currentSpeed = 0;                   //++
    public uint upgradePassiveSpeedIncrease = 1;    //++
    public uint upgradeActiveSpeedIncrease = 1;     //++
    public uint skinsPassiveSpeedIncrease = 0;      //++
    public uint skinsActiveSpeedIncrease = 0;       //++
    public int coins = 0;                           //++
    public uint overallSpeed = 50;                  //++
    //Скины
    public int selectedHatId = 0;                   //++
    public int selectedPetId = 0;                   //++
    public int selectedTrailId = 0;                 //++

    public bool[] hatSkinsBuyStates = new bool[90];                //++
    public bool[] petSkinsBuyStates = new bool[43];                //++
    public bool[] trailSkinsBuyStates = new bool[13];              //++
    public bool[] areLevelsUnlock = new bool[5];                  //++
}

public class Bank : MonoBehaviour
{
    public static Bank Instance;
    public PlayerInfo playerInfo; 
    private YandexSDK yandexSDK;

    bool firstLaunch = true;
    private void Awake()
    {
        yandexSDK = FindObjectOfType<YandexSDK>();
       

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            yandexSDK.Load();          
            return;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!firstLaunch)
            return;

        if (YandexSDK.dataIsLoaded)
        {
            Instance.playerInfo.hatSkinsBuyStates[0] = true;
            Instance.playerInfo.petSkinsBuyStates[0] = true;
            Instance.playerInfo.trailSkinsBuyStates[0] = true;
            Instance.playerInfo.areLevelsUnlock[0] = true;
            firstLaunch = false;
        }
    }
}
