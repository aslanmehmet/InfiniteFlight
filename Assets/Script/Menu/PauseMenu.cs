using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

     public Canvas canvas;

	 public void PauseGame()
     {
        canvas.gameObject.SetActive(true);
        Time.timeScale = 0;
     }

    public void ResumeGame()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }




}
