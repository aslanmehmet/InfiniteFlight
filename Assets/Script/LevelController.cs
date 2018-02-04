using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelController : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public Transform player;
    public Factory fcScript;
    public Player_Controller playerControl;

    [Space]

    [Header("Material")]
    public Material floorMat;
    public Material enemyMat;

    [Header("Score")]
    public float missionScore = 500;

    [Space]

    [Header("WaitSeconds")]
    public float waitSeconds;

    Color newFloorColor;
    Color oldFloorColor;
    Color newEnemyColor;
    Color oldEnemyColor;

    GameObject[] factorys;

    float lastScore;
    float score = 0;
    float levelScore = 5;
    int level = 0;
    int loopLevel = 0;

    private void Start()
    {
        //Renk Ataması
        SetOldColor();

        //Levelin Baslangıc degeri Atanması
        //Continues yaptıysa
        if (PlayerPrefs.GetInt("Resume") == 1)
        {
            //Kaldıgı level ve score bilgileri alınır.
            level = PlayerPrefs.GetInt("ResumeLevel") - 1;
            lastScore = PlayerPrefs.GetFloat("Score");
            //Kaldıgı loop level alınır buna göre hızı atanır.
            playerControl.ChangeSpeed(PlayerPrefs.GetInt("LoopLevel"));
        }
       
         //Yaratılan Factorylerin Diziye Atılması
        Invoke( "GetFactory" ,0.2f);
        
        //First Level Yukselmesi
        LevelAdd();
    }

    private void Update()
    {
        //Score Gösterilmesi
        Score();

        //Level Kontrol edilmesi
        ScoreController();

        //Renklerin Yavasca Degismesi
        ColorLerp();
    }

    void Score()
    {
        
        //Playerin z positionu alınur
        float scorePos = player.position.z;
        //Son kaldıgı skor var ise score degişkenine eklenir.
        score = scorePos + lastScore ;

        //Prefste kayıt edilir. 
        PlayerPrefs.SetFloat("Score",score);
        //Texte yazılr.
        scoreText.text = score.ToString("0");
        //scoreText.text  = string.Format("{0:#,#0}", score);

       
    }
  

    void ScoreController()
    {
        //Score , Level Skorunu geçerse 
        if (score > levelScore )
        {
            //Leveli Geçirt.
           StartCoroutine( LevelAdd()) ;
        }
    }

    IEnumerator LevelAdd()
    {
        //Level Yukselir
        level++;
        //Level Bilgisi Prefs atalır.
        PlayerPrefs.SetInt("ResumeLevel", level);

        //Leveli Gecmek için istenilen Skora belirlenir
        levelScore += missionScore;

        //Factory objelerinin durması için Brigce sinyanli gönderilir.
        LevelBreak();
        
        //Player blokları gecmesi için fonksiyon bekletilir.
        yield return new WaitForSeconds(waitSeconds);

        //Renkler Degistirilir.
        ChangeColor();

        //Level Degistirilir.
        ChangeLevel();
    }

    //Sıradaki leveli belirler
    void ChangeLevel()
    {
        if (level == 7)
        {
            level = 0;

            //Kaç Tekrara Girdigi
            loopLevel++;
            PlayerPrefs.SetInt("LoopLevel", loopLevel);

            //Bolumu geçmesi için istenilen skora artırılyor.
            missionScore += 400;

            //Geminin Hızının Artırılması
            playerControl.ChangeSpeed(loopLevel);
         }
         //Level secimini yapar.
         SelectLevel(level);
    }

    void SelectLevel(int level)
    {
        switch (level)
        {
            case 1:
                //EnemyCount
                DecreaseEnemyCount(2);
                //EnemySize
                ChangeSize(1,3);
                //EnemyAnim
                EnemyAnim(false , false);
                
                break;


            case 2:
                //EnemyCount
                IncreaseEnemyCount();
                DecreaseEnemyCount(1);
                //EnemySize
                ChangeSize(4, 7);
                //EnemyAnim
                EnemyAnim(false , false);

                break;

            case 3:
                //EnemyCount
                IncreaseEnemyCount();
                DecreaseEnemyCount(1);
                //EnemySize
                ChangeSize(2, 7);
                //EnemyAnim
                EnemyAnim(false , false);


                break;

            case 4:
                //EnemyCount
                IncreaseEnemyCount();
                DecreaseEnemyCount(2);
                //EnemySize
                ChangeSize(3 , 6);
                //EnemyAnim
                EnemyAnim(true , true);
                break;

            case 5:
                //EnemyCount
                IncreaseEnemyCount();
                DecreaseEnemyCount(1);
                //EnemySize
                ChangeSize(5, 9);
                //EnemyAnim
                EnemyAnim(true , false);

                break;

            case 6:
                //EnemyCount
                IncreaseEnemyCount();
                DecreaseEnemyCount(1);
                //EnemySize
                ChangeSize(8, 9);
                //EnemyAnim and Dir true y
                EnemyAnim(true, true);

                break;

            default:
                break;
        }
    }

   List<List<GameObject>> readyEnemy = new List<List<GameObject>>();
    
    //Enemy Animasyon
    void EnemyAnim(bool state,bool dirY)
    {
        foreach (var item in factorys)
        {
            item.GetComponent<Factory>().isAnim = state;
            item.GetComponent<Factory>().animDir = dirY;
        }
    }

    //Enemyleri yok et.
    void DecreaseEnemyCount(int aliveEnemyNubmer)
    {
        //Factory içine girer.
        foreach (var fac in factorys)
        {
            //Yok Edilecek enemySayısı bulunır.
            int destroyEnemyCount = fac.GetComponent<Factory>().enemy_Count - aliveEnemyNubmer;
            //Yok edilenleri tekrar cagırmak için bir dizi olusuturulur.
            List<GameObject> enemyInFac = new List<GameObject>();
            //Yok etme islemi
            int count = 0;
            foreach (var item in fac.GetComponent<Factory>().enemyArray)
            {
                count++;
                //istenilen kadar yok edildi mi?
                if (count > destroyEnemyCount)
                {
                    break;//Çık
                }

                //Yok edileni diziye ekle
                enemyInFac.Add(item);
                //Yok Et
                item.SetActive(false);
            }
            //Yok edilenler dizisini hazır dizisine ekle.
            if ( enemyInFac != null )
            {
                readyEnemy.Add(enemyInFac);
            }
            
        }
    }

    //Ready olan enemyleri geri oyuna sok.
    void IncreaseEnemyCount()
    {
        if ( readyEnemy == null)
        {
            return;
        }

        foreach (var read in readyEnemy)
        {
            foreach (var item in read)
            {
                item.SetActive(true);
            }
        }
        readyEnemy.Clear();
    }

    //Enemy Sizelarını Degistirme.
    void ChangeSize(float min , float max)
    {
        foreach (var fac in factorys)
        {
            fac.GetComponent<Factory>().min_Lenght = min;
            fac.GetComponent<Factory>().max_Length = max;

        }
    }

    //Level Geçisi İçin zaman verir.
    float breakTime = 5f;
    void LevelBreak()
    {
        if (factorys != null)
        {
            foreach (var factory in factorys)
            {
                factory.GetComponent<Factory>().isLevelBridge(true);
            }
        }
        else
        {
            Debug.Log("Factory Not Found");
        }

        Invoke("OverLevelBreak", breakTime);
    }
    void OverLevelBreak()
    {
        if (factorys != null)
        {
            foreach (var factory in factorys)
            {
                factory.GetComponent<Factory>().isLevelBridge(false);
            }
        }
        else
        {
            Debug.Log("Factory Not Found");
        }

    }

    //Factory Objelerinin Bulunması ve Diziye atılması
    void GetFactory()
    {
        factorys = GameObject.FindGameObjectsWithTag("Factory");
    }

    //oldColorlara Renk Atamsı
    private void SetOldColor()
    {
        oldFloorColor = floorMat.color;
        oldEnemyColor = enemyMat.color;
    }

    //Yeni atamaları.
    void ChangeColor()
    {
        //Floor ve Enemy için yeni renkler belirlernir.
        //Döngüler renklerin aynı olmaması için
        while (true)
        {
            newFloorColor = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), 1);
            if (oldFloorColor != newEnemyColor && newFloorColor != Color.black)
            {
                break;
            }
            else
            {
                Debug.Log("FloorColor Aynı Geldi ");
            }
        }

        while (true)
        {
            newEnemyColor = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), 1);
            if (newFloorColor != newEnemyColor && newEnemyColor != oldEnemyColor && newEnemyColor != Color.black)
            {
                break;
            }
            else
            {
                Debug.Log("EnemyColor aynı veya floor ile aynı geldi");
            }
        }
        oldFloorColor = newFloorColor;
        oldEnemyColor = newEnemyColor;

    }

    //Lerp ile renkleri Degistir.
    void ColorLerp()
    {
        floorMat.color = Color.Lerp(floorMat.color , oldFloorColor, Time.deltaTime);
        enemyMat.color = Color.Lerp(enemyMat.color , oldEnemyColor, Time.deltaTime );
    }
    
}
