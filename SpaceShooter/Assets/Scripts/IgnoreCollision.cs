using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(8 /*Player*/, 9 /*Bullets*/);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
