using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Position : MonoBehaviour {

    public Transform Player;
    Vector3 offset;


    void Start () {

        offset = Player.position - transform.position;
	}
	
	
	void Update () {

        this.transform.position = Player.transform.position - offset;
	}
}
