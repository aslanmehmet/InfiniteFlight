using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour {

    GameObject menuMusic;
    bool oldMenuMusicState;

    void MenuMusicOff()
    {
        menuMusic = GameObject.FindGameObjectWithTag("menuMusic");
        if (menuMusic == null)
            return;

        oldMenuMusicState = menuMusic.GetComponent<AudioSource>().enabled;
        menuMusic.GetComponent<AudioSource>().enabled = false;
    }

    private void Awake()
    {
        MenuMusicOff();
    }

    private void OnDestroy()
    {
        if(menuMusic != null)
            menuMusic.GetComponent<AudioSource>().enabled = oldMenuMusicState;
    }

   
}
