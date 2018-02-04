using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Collider : MonoBehaviour {

    public GameObject particalPrefabs;
    public LevelController levelControl;
    
    public GameObject gameoverText;
    public TextMeshProUGUI coinText;

    public Canvas mainCanvas;

    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreDead;

    public TextMeshProUGUI continueCoinText;

    int coinCount = 0; //Oyun içi topladıgı altın
    int coinValue = 10; //Bir altının degeri

    public bool isShild = false; //Kalkanlı mi?

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "Enemy" )
        {
            //Kalkanı yoksa
            if (!isShild)
            {
                PlayerDead();
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            Collect_Coin();
        }
    }

    int continuePrice;
    int basePrice = 200;

    //Playerin Ölmesi
    void PlayerDead()
    {   
        //Özelliklerinin Kapatılması
        GetComponent<Player_Controller>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        //GameOver Text Gösterilmesi
        Invoke("ShowGameOver", 1f);

        //Geminin Parcalanması
        Split_Player(this.gameObject, particalPrefabs);
        //Topladıgı Coinlerin Kayıt edilmesi
        SetCoin();


        //Player Ölüm Sayısna göre bedel
        continuePrice = basePrice * System.Convert.ToInt32( (Mathf.Pow(2, PlayerPrefs.GetInt("deadCount"))));
        PlayerPrefs.SetInt("deadCount", PlayerPrefs.GetInt("deadCount") + 1 );
        continueCoinText.text ="- "+ continuePrice.ToString();
        PlayerPrefs.SetInt("deadPrice", continuePrice);


        //Main canvas kapanır.
        CanvasHide( mainCanvas );

        PlayGamesScript.SetHighScore(System.Convert.ToInt64(PlayerPrefs.GetFloat("Score")));

    }

    void CanvasHide(Canvas c)
    {
        c.gameObject.SetActive(false);
        scoreDead.text = score.text;
    }

    //GameOver Ekranı Gösterilmesi
    void ShowGameOver()
    {
        gameoverText.gameObject.SetActive(true);
    }

    //Obje Parcalanması
    void Split_Player(GameObject player, GameObject partical)
    {

        Renderer objeRenderer = player.GetComponent<Renderer>();
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;

        player.gameObject.SetActive(false);
        
        float x = objeRenderer.bounds.extents.x; // Split yapılacak objenin merkezden sınırlarına olan mesafeleri alınır.
        float y = objeRenderer.bounds.extents.y;
        float z = objeRenderer.bounds.extents.z;
        
        float size = 2 * partical.GetComponent<Renderer>().bounds.extents.x;

        Vector3 center = new Vector3(objeRenderer.bounds.center.x, objeRenderer.bounds.center.y, objeRenderer.bounds.center.z);

        for (float i = center.x - x + (size / 2); i <= center.x + x; i += size) //artıs miktarlarını olusturmak istedigin parca sizeları kadar belirle
        {
            for (float k = center.y - y + (size / 2); k <= center.y + y; k += size)
            {
                for (float j = center.z + (size / 2) - z; j <= center.z + z; j += size)
                {
                    GameObject g = Instantiate(partical);
                    g.transform.position = new Vector3(i, k, j);
                    if ( velocity.x != 0)
                    {
                        g.GetComponent<Rigidbody>().AddForce(new Vector3(-velocity.x * Random.Range(30f, 60f), 0, 1000f));
                    }
                    else
                    {
                        g.GetComponent<Rigidbody>().AddForce(new Vector3( Random.Range(-600f, 600f), 0, 1000f));
                    }
                   
                }
            }
        }

    }

    //Oyun içinde Coin Toplama
    void Collect_Coin()
    {
        coinCount += coinValue;
        coinText.text = coinCount.ToString();
    }

    //Coinleri PlayerPrefs içine ekliyoruz.
    void SetCoin()
    {
        int totalCoin = PlayerPrefs.GetInt("COIN") + coinCount;
        PlayerPrefs.SetInt("COIN",totalCoin);
    }

  
}
