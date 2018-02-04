using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    GameObject[] menuMusic;

    private void Awake()
    {
        menuMusic = GameObject.FindGameObjectsWithTag("menuMusic");
        if (menuMusic.Length > 1)
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }
}
