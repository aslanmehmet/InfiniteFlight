using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game_Manager : MonoBehaviour {


    void Sets()
    {
        UIScript.Instance.CloudSetCoin();
        UIScript.Instance.CloudSetXSpeed();
        UIScript.Instance.CloudSetPower();
    }

    public static void Gets()
    {
        Debug.Log("Gets() Girdi");
        UIScript.Instance.CloudGetCoin();
        UIScript.Instance.CloudGetXSpeed();
        UIScript.Instance.CloudGetPower();
    }

   

    private void OnDestroy()
    {
        Sets();
        UIScript.Instance.Save();  
    }
}
