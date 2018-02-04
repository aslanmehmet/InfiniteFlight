using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSkills : MonoBehaviour {

    public TextMeshProUGUI powerText;

    [Header("Bullet")]
    public GameObject bulletPrefabs;
    float bulletSpeed;
    GameObject bullet;

    [Header("SlowMonition")]
    Player_Controller playerControl;
    float slowSpeed;
    float normalSpeed;
    float slowTime = 2f;

    [Header("Shild")]
    public GameObject shildPrefabs;
    GameObject shild;
    float shildTime = 3f ;

    int powerCount;

    private void Awake()
    {
        playerControl = this.GetComponent<Player_Controller>();

        GetPowerCount();
    }

    #region Skill
    void Shoot()
    {
        //Kursunun Yaratılması
        bullet =  Instantiate(bulletPrefabs, transform.position + new Vector3(0,0,3), Quaternion.identity);

        //Geminin Hızından geri kalmaması için bulletSpeed Ayarlanır.
        bulletSpeed = 150 * playerControl.speed;

        //Kursuna Hız verilmesi
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * bulletSpeed * Time.deltaTime);
    }

    bool isSlow = false;
    void SlowMonition()
    {
        isSlow = true;
        normalSpeed = playerControl.speed;
        slowSpeed = normalSpeed / 2;
        playerControl.speed = slowSpeed;

        Invoke("NormalSpeed" , slowTime);
    }

    void NormalSpeed()
    {
        playerControl.speed = normalSpeed;
        isSlow = false;
    }

    bool isShild = false;
    void Shild()
    {
        isShild = true;
        this.GetComponent<Player_Collider>().isShild = true;
        shild = Instantiate(shildPrefabs, transform.position, Quaternion.identity);
        shild.transform.parent = transform;

        GetComponent<Collider>().enabled = false;
        Invoke("ShildisFalse", 3.2f);
    }

    void ShildisFalse()
    {
        isShild = false;
        Destroy(shild.gameObject);
        GetComponent<Collider>().enabled = true;
    }

    #endregion

    //Power Kullanır.
    void UsePower(int shipIndex)
    {
        switch (shipIndex)
        {
            case 0:
                Shoot();
                powerCount--;
                break;


            case 1:
                if (!isShild)
                {
                    Shild();
                    powerCount--;
                }
                
                break;


            case 2:
                if (!isSlow)
                {
                    SlowMonition();
                    powerCount--;
                }
                break;

            default:
                break;
        }
    }

    //Ship Index bilgisini alır.
    int GetShipIndex()
    {
        if (PlayerPrefs.HasKey("ShipIndex"))
        {
            return PlayerPrefs.GetInt("ShipIndex");
        }
        else
            return 0;
        
    }

   
    private void LateUpdate()
    {
        ShowPowerCount();
    }

    public void ButtonPower()
    {
        if (powerCount > 0)
        {
            UsePower(GetShipIndex());
           
        }
    }

    //Power Count alınması
    void GetPowerCount()
    {
        powerCount = PlayerPrefs.GetInt(GetShipIndex() + "powerCount");
    }

    //Power Count Ekranda Gösterilmesi
    void ShowPowerCount()
    {
        powerText.text = powerCount.ToString(); 
    }
}
