using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerAdd : MonoBehaviour {

    public Scrollbar scrollBar;
    public TextMeshProUGUI attributeText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinText;

    //Scroll Barı Efekti için
    float valueSize = 0.1f;

    //Degerlerin artması
    int value = 10; //Deger
    int accrual = 10;   //Artma Oranı

    //Price
    int[] price = { 1000, 5000, 10000, 20000, 50000, 100000, 200000, 500000, 1000000 };
    int priceIndex = 0;


    private void LateUpdate()
    {
          TextGuncelle();
    }

    void TextGuncelle()
    {
        if (getPowerCount(GetShipIndex()) == 0)
        {
            setPowerCount(GetShipIndex(), 10);
        }

        value = getPowerCount(GetShipIndex());
        float size = value / 10f;
        scrollBar.size = size ;
       // Debug.Log(scrollBar.size.ToString());
        value *= 10;
        attributeText.text = "%" + value.ToString();

        priceIndex = value / 10 - 1;
        priceText.text = string.Format("{0:#,##0}", price[priceIndex]);


    }

    //Power Artırılması
    public void ValueRise_Power()
    {
        if (value < 100)
        {
            if (price[priceIndex] <= PlayerPrefs.GetInt("COIN"))
            {

                SpendCoin();
                ChangeCoinText();

                scrollBar.size += valueSize;

                value += accrual;

                //Ship 
                int index = PlayerPrefs.GetInt("ShipIndex");
                setPowerCount(index, value);

                attributeText.text = "%" + value.ToString();

                priceIndex++;
                if (priceIndex <= price.Length - 1)
                {
                    priceText.text = price[priceIndex].ToString("0");
                }
                else
                {
                    priceText.text = "FULL";
                }
            }
        }
    }

    //PowerCount playerPref atılır.
    void setPowerCount(int index, int value)
    {
        PlayerPrefs.SetInt(index + "powerCount", value / 10);
    }

    int getPowerCount(int shipIndex)
    {
        return PlayerPrefs.GetInt(shipIndex + "powerCount");
    }

    //Coinin Harcanması
    void SpendCoin()
    {
        int totalCoin = PlayerPrefs.GetInt("COIN") - price[priceIndex];
        PlayerPrefs.SetInt("COIN", totalCoin);
    }

    //Coin Text Guncelleme
    void ChangeCoinText()
    {
        coinText = GameObject.FindGameObjectWithTag("coinText").GetComponent<TextMeshProUGUI>();
        ShowCoin();
    }

    //Coin Text Guncelleme
    void ShowCoin()
    {
        coinText.text = PlayerPrefs.GetInt("COIN").ToString();
    }

    int GetShipIndex()
    {
        return PlayerPrefs.GetInt("ShipIndex");
    }

}
