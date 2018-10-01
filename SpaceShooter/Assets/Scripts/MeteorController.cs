using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

    public float maxhealth = 200f;
    public float currenthealth;


	// Use this for initialization
	void Start ()
    {
        currenthealth = maxhealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        destroymeteor();
        maxhealthcap();
    }

    void destroymeteor()
    {
        if (currenthealth < 0)
        {
            currenthealth = 0;
            Destroy(gameObject);
        }
    }
    void maxhealthcap()
    {
        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
    }
}
