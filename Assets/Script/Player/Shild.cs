using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour {

    float time = 3f;
    Player_Collider playerColider;

    private void Start()
    {
        playerColider = transform.parent.GetComponent<Player_Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            SelfDestruct();
        }
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
        playerColider.isShild = false;
    }


    private void Update()
    {
        Invoke("SelfDestruct", time);
    }
}
