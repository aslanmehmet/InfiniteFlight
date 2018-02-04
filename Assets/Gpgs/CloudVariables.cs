using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVariables : MonoBehaviour {

    public static int HighScore{ get; set; }
	public static int[] ImportantValues{ get; set; }

	private void Awake(){
		ImportantValues = new int[7];
	}

}
