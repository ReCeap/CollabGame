using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public float bulletdamage;
    public Playermovement player;

    void Start()
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        player = pl.GetComponent<Playermovement>();
    }

    void Update()
    {
        changebulletdamage();
    }

    void changebulletdamage()
    {
        if (player.ActiveGun == "Gun00")
        {
            bulletdamage = 30f;
        }
        if (player.ActiveGun == "Gun01")
        {
            bulletdamage = 50f;
        }
        if (player.ActiveGun == "Gun02")
        {
            bulletdamage = 40f;
        }
        if (player.ActiveGun == "Gun03")
        {
            bulletdamage = 12.5f;
        }
        if (player.ActiveGun == "Gun04")
        {
            bulletdamage = 45f;
        }
        if (player.ActiveGun == "Gun05")
        {
            bulletdamage = 20f;
        }
        if (player.ActiveGun == "Gun06")
        {
            bulletdamage = 30f;
        }
        if (player.ActiveGun == "Gun07")
        {
            bulletdamage = 70f;
        }
        if (player.ActiveGun == "Gun08")
        {
            bulletdamage = 7.5f;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(c.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (c.gameObject.tag == "Meteor")
        {
            c.gameObject.GetComponent<MeteorController>().currenthealth -= bulletdamage;
            Destroy(gameObject);
        }
    }
}