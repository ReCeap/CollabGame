using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebullet : MonoBehaviour {

    float movespeed = 0.3f;

    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    void FixedUpdate()
    {
        transform.position += transform.up * movespeed;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
