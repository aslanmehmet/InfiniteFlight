using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float time = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
        

    private void Update()
    {
        Invoke("SelfDestruct" , time );
    }
}
