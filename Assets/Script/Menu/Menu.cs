using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public Sprite imgMusicOn;
    public Sprite imgMusicOff;
    public Button btnMusic;

    private void Start()
    {
        if (coinText != null)
        {
            ShowCoin();
        }else
        {
            Debug.Log("Coin Text Not Found");
        }

        //Music
        menuMusic = GameObject.FindGameObjectWithTag("menuMusic");
        if (menuMusic != null)
        {
            isMusicOn = menuMusic.GetComponent<AudioSource>().enabled;
        }
        ChangeImgMusic(isMusicOn);
    }

    void ShowCoin()
    {

        coinText.text = string.Format("{0:#,##0}", PlayerPrefs.GetInt("COIN"));
        
    }

    [Header("Loading")]
    public GameObject mainCanvas;
    public GameObject loadingCanvas;
    public Scrollbar slider;

    public void PlayGame()
    {
        PlayerPrefs.SetInt("deadCount",0);
        isContinues((int)continues.no);

        StartCoroutine(LoadAsynchronously());
        
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");

        loadingCanvas.SetActive(true);
        mainCanvas.SetActive(false);

        while (!operation.isDone)
        {
            float pro = Mathf.Clamp01(operation.progress / .9f);

            slider.size = pro;

            yield return null;
        }
    
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StoreEnter()
    {
        SceneManager.LoadScene("Store");
    }

    public void GarageEnter()
    {
        SceneManager.LoadScene("Garage");
    }

    public void ContinuesGame()
    {
        if (PlayerPrefs.GetInt("deadPrice") <= PlayerPrefs.GetInt("COIN"))
        {
            Debug.Log(PlayerPrefs.GetInt("COIN").ToString());
            PlayerPrefs.SetInt("COIN",PlayerPrefs.GetInt("COIN") - PlayerPrefs.GetInt("deadPrice"));
            Debug.Log(PlayerPrefs.GetInt("COIN").ToString());

            isContinues((int)continues.yes);
            SceneManager.LoadScene("Main");
        }

    }
    
    enum continues
    {
        no,
        yes
    }

    void isContinues(int value)
    {
        PlayerPrefs.SetInt("Resume", value);
    }

    bool isMusicOn;
    GameObject menuMusic;
    public void BtnChangeMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
            menuMusic.GetComponent<AudioSource>().enabled = true;
        else
            menuMusic.GetComponent<AudioSource>().enabled = false;

        ChangeImgMusic(isMusicOn);
    }
    
    void ChangeImgMusic(bool state)
    {
        if (btnMusic == null) { return; }

        if (state)
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        else
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
    }
 
}
