using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    public GameObject slider;

    public Button btnLeft;
    public Button btnRight;

    int shipCount;
    int maxIndex, minIndex, index;

    private void Start()
    {
        shipCount = slider.transform.childCount;
        maxIndex = shipCount - 1;
        minIndex = 0;
        index = 0;

        setShipIndex();

        ShowShip(index);
    }

    void ShowShip(int index)
    {
        HideShip();
        slider.transform.GetChild(index).gameObject.SetActive(true);
    }

    void HideShip()
    {
        for (int i = 0; i < shipCount; i++)
        {
            slider.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void StateBtn(Button btn , bool state)
    {
        btn.gameObject.SetActive(state);
    }

    public void BtnNext()
    {
        index++;
        if (index <= maxIndex)
            ShowShip(index);
        else
        {
            index = maxIndex;
           
        }

        if (index == maxIndex)
        {
            StateBtn(btnRight, false);
        }
        

        StateBtn(btnLeft, true);

        setShipIndex();
    }

    public void BtnPre()
    {
        index--;
        if (index >= minIndex)
            ShowShip(index);
        else
            index = minIndex;

        if (index == minIndex)
        {
            StateBtn(btnLeft, false);
        }
        

        StateBtn(btnRight, true);

        setShipIndex();
    }

    public void BtnSelect()
    {
        setShipIndex();
        BackMenu();
    }
    
    //ShipIndex Alınır.
    void setShipIndex()
    {
        PlayerPrefs.SetInt("ShipIndex", index);
    }
    
    void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}