using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour {

    public static UIScript Instance { get; private set; }
    public TextMeshProUGUI coinText;

    void Awake()
    {
        Instance = this;
    }
    
    enum Variables
    {
        coin,
        slowShipXSpeed,
        slowShipSpecial,
        bulletShipXSpeed,
        bulletShipSpecial,
        shieldShipXSpeed,
        shieldShipSpecial
    }

    #region Coin
    //Get Coin
    public void CloudGetCoin()
    {
            int val = CloudVariables.ImportantValues[(int)Variables.coin];
            Debug.Log("başlayınca coin: " + val);
            PlayerPrefs.SetInt("COIN", val);
        CoinText();
    }


    public void CloudSetCoin()
    {
        int val = PlayerPrefs.GetInt("COIN");
        CloudVariables.ImportantValues[(int)Variables.coin] = val;
    }
    #endregion /Coin


    public void CloudSetXSpeed()
    {
        int val = PlayerPrefs.GetInt("0" + "xSpeed");
        int val1 = PlayerPrefs.GetInt("1" + "xSpeed");
        int val2 = PlayerPrefs.GetInt("2" + "xSpeed");


            CloudVariables.ImportantValues[(int)Variables.bulletShipXSpeed] = val;
        
            CloudVariables.ImportantValues[(int)Variables.shieldShipXSpeed] = val1;
        
            CloudVariables.ImportantValues[(int)Variables.slowShipXSpeed] = val2;
    }

    public void CloudGetXSpeed()
    {
             int val = CloudVariables.ImportantValues[(int)Variables.bulletShipXSpeed];
             PlayerPrefs.SetInt("0xSpeed", val);

            int val1 = CloudVariables.ImportantValues[(int)Variables.shieldShipXSpeed];
            PlayerPrefs.SetInt("1xSpeed", val1);

     
            int val2 = CloudVariables.ImportantValues[(int)Variables.slowShipXSpeed];
            PlayerPrefs.SetInt("2xSpeed", val2);

    }

    public void CloudSetPower()
    {
        int val = PlayerPrefs.GetInt("0" + "powerCount");
        int val1 = PlayerPrefs.GetInt("1" + "powerCount");
        int val2 = PlayerPrefs.GetInt("2" + "powerCount");

            CloudVariables.ImportantValues[(int)Variables.bulletShipSpecial] = val;
            CloudVariables.ImportantValues[(int)Variables.shieldShipSpecial] = val1;
            CloudVariables.ImportantValues[(int)Variables.slowShipSpecial] = val2;

    }

    public void CloudGetPower()
    {
            int val = CloudVariables.ImportantValues[(int)Variables.bulletShipSpecial];
            PlayerPrefs.SetInt("0powerCount", val);

            int val1 = CloudVariables.ImportantValues[(int)Variables.shieldShipSpecial];
            PlayerPrefs.SetInt("1powerCount", val1);
 
            int val2 = CloudVariables.ImportantValues[(int)Variables.slowShipSpecial];
            PlayerPrefs.SetInt("2powerCount", val2);
        
    }



    //Save Data
    public void Save()
    {
        PlayGamesScript.Instance.SaveData();
        
    }

    private void CoinText()
    {
        if (coinText != null)
        {
            coinText.text = string.Format("{0:#,##0}", PlayerPrefs.GetInt("COIN"));
        }
    }

    public void ShowLeaderBoard()
    {
        PlayGamesScript.ShowLeaderboardUI();
    }
}
