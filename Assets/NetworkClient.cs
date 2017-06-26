using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Network.Connect("127.0.0.1", 4999, "HolyMoly");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
