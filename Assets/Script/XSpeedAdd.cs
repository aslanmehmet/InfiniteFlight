using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XSpeedAdd : MonoBehaviour {

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
    int[] price = { 1000 , 5000 , 10000 , 20000 , 50000 , 100000 , 200000 , 500000 , 1000000};
    int priceIndex=0;

   private void LateUpdate()
    {
      TextGuncelle();
    }

    void TextGuncelle()
    {
        if (getXSpeed(GetShipIndex()) == 0)
        {
            setXSpeed(GetShipIndex(), 10);
        }

        value = getXSpeed(GetShipIndex());
        float size = value / 10f;
        scrollBar.size = size;
        value *= 10;
        attributeText.text = "%" + value.ToString();

        priceIndex = value / 10 - 1;
        priceText.text = string.Format("{0:#,##0}", price[priceIndex] );

    }

    //X Speed Arttırılması
    public void ValueRise_xSpeed()
    {
        //Deger Full değilse
        if (value < 100)
        {   
            //Yeterli Coin Bulunuyorsa
            if (price[priceIndex] <= PlayerPrefs.GetInt("COIN"))
            {
                
                SpendCoin();
                ChangeCoinText();
                
                //Yesil Barın Dolması
                scrollBar.size += valueSize;
              
                //Yuzdelik Yazının Degerinin Artırılması
                value += accrual;
                attributeText.text = "%" + value.ToString();

                //Özelligin Prefabsa atılması
                setXSpeed(GetShipIndex(), value);

                //Ödeme Yapıldıgı için diger ödeme bedeli artırılır.
                priceIndex++;
                
                //Yeni Fiyat Bedeli Ekrana Yazdırılır.
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

    //xSpeed playerPref atılır.
    void setXSpeed(int index , int value)
    {
        PlayerPrefs.SetInt( index+"xSpeed" , value/10 );
    }

    int getXSpeed(int shipIndex)
    {
        return PlayerPrefs.GetInt(shipIndex + "xSpeed" );
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

    void ShowCoin()
    {

        coinText.text = string.Format("{0:#,##0}", PlayerPrefs.GetInt("COIN"));

    }

    int GetShipIndex()
    {
       return PlayerPrefs.GetInt("ShipIndex");
    }

}
