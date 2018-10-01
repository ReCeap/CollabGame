using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour {

#region Vars
    [SerializeField]
    private float speed;

    public Transform pl;

    protected Vector2 direction;
    private Rigidbody2D rb;
    private float yAxis;
    public float maxVelocity = 5f;

    public string ActiveGun;
    public float shootingcooldown;
    public float guncooldown;

    public GameObject guns;

    public float vx;
    public float vy;
    public float fvx;
    public float fvy;

    float maxplayerhealth = 250f;
    public float currentplayerhealth;
    public float absvelocitycount;

    public float meteorcollidedmg = 20;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
    #region BulletGameObjects

    public GameObject gun00Bullet;
    public GameObject gun01Bullet;
    public GameObject gun02Bullet;
    public GameObject gun03Bullet;
    public GameObject gun04Bullet;
    public GameObject gun05Bullet;
    public GameObject gun06Bullet;
    public GameObject gun07Bullet;
    public GameObject gun08Bullet;

    #endregion


    #endregion

#region Start&Update
    // Use this for initialization
    void Start () {
        //Makes a reference to the rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        pl = GetComponent<Transform>();
        currentplayerhealth = maxplayerhealth;
    }
	
	// Update is called once per frame
	void Update () {
        Shipmove(yAxis);
        faceMouse();
        ShootingInput();
        cooldowntimer();
        ClampVelocity();
        rbvelo();
        ChangeGunStats();
        absvelocitycount = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        deathhandel();
        brokestates();
    }
    #endregion

#region Voids

    void brokestates()
    {
        if(currentplayerhealth < maxplayerhealth * 0.85)
        {
            pl.GetChild(0).GetChild(5).gameObject.SetActive(true);
            pl.GetChild(0).GetChild(4).gameObject.SetActive(false);
            pl.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        if (currentplayerhealth < maxplayerhealth * 0.50)
        {
            pl.GetChild(0).GetChild(5).gameObject.SetActive(false);
            pl.GetChild(0).GetChild(4).gameObject.SetActive(true);
            pl.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        if (currentplayerhealth < maxplayerhealth * 0.25)
        {
            pl.GetChild(0).GetChild(5).gameObject.SetActive(false);
            pl.GetChild(0).GetChild(4).gameObject.SetActive(false);
            pl.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }
    }

    void deathhandel()
    {
        if(currentplayerhealth < 0)
        {
            currentplayerhealth = 0;
            Destroy(gameObject);
        }
        if(currentplayerhealth > maxplayerhealth)
        {
            currentplayerhealth = maxplayerhealth;
        }
    }

    void OnCollisionEnter2D (Collision2D c)
    {
        if (c.gameObject.tag == "Meteor")
        {
            currentplayerhealth -= meteorcollidedmg * absvelocitycount;
        }
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.up = direction;
    }

    void rbvelo()
    {
        vx = rb.velocity.x;
        vy = rb.velocity.y;

        yAxis = Input.GetAxis("Vertical");
    }

    void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    void Shipmove(float amount)
    {
        if (Input.GetKey(KeyCode.W))
            {
            Vector2 force = transform.up * amount;

            rb.AddForce(force);
            }
        if (Input.GetKey(KeyCode.S))
        {
            if(vx < 0)
            {
                fvx = vx + Time.deltaTime;
                if(vx > 0.012)
                {
                    vx = 0;
                    fvx = 0;
                }
            }
            if (vx > 0)
            {
                fvx = vx - Time.deltaTime;
                if (vx < 0.012)
                {
                    vx = 0;
                    fvx = 0;
                }
            }
            if (vy < 0)
            {
                fvy = vy + Time.deltaTime;
                if (vy > 0.012)
                {
                    vy = 0;
                    fvy = 0;
                }
            }
            if (vy > 0)
            {
                fvy = vy - Time.deltaTime;
                if (vy < 0.012)
                {
                    vy = 0;
                    fvy = 0;
                }
            }
            rb.velocity = new Vector2(fvx, fvy);
        }
    }

    void cooldowntimer()
    {
        shootingcooldown -= Time.deltaTime;
        if(shootingcooldown < 0)
        {
            shootingcooldown = 0;
        }
    }
    #region Guns&Shooting
    void ShootingInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot();
        }
        if (Input.GetKey(KeyCode.E)) // Activate Gun00 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(true);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun00";
        }
        if (Input.GetKey(KeyCode.R)) // Activate Gun01 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(true);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun01";
        }
        if (Input.GetKey(KeyCode.T)) // Activate Gun02 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(true);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun02";
        }
        if (Input.GetKey(KeyCode.Z)) // Activate Gun03 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(true);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun03";
        }
        if (Input.GetKey(KeyCode.U)) // Activate Gun04 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(true);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun04";
        }
        if (Input.GetKey(KeyCode.I)) // Activate Gun05 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(true);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun05";
        }
        if (Input.GetKey(KeyCode.O)) // Activate Gun06 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(true);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun06";
        }
        if (Input.GetKey(KeyCode.P)) // Activate Gun07 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(true);
            guns.transform.GetChild(8).gameObject.SetActive(false);
            ActiveGun = "Gun07";
        }
        if (Input.GetKey(KeyCode.F)) // Activate Gun08 Debugging only
        {
            guns.transform.GetChild(0).gameObject.SetActive(false);
            guns.transform.GetChild(1).gameObject.SetActive(false);
            guns.transform.GetChild(2).gameObject.SetActive(false);
            guns.transform.GetChild(3).gameObject.SetActive(false);
            guns.transform.GetChild(4).gameObject.SetActive(false);
            guns.transform.GetChild(5).gameObject.SetActive(false);
            guns.transform.GetChild(6).gameObject.SetActive(false);
            guns.transform.GetChild(7).gameObject.SetActive(false);
            guns.transform.GetChild(8).gameObject.SetActive(true);
            ActiveGun = "Gun08";
        }

    }

    void ChangeGunStats()
    {
        if (ActiveGun == "Gun00")
        {
            guncooldown = 0.3f;
        }
        if (ActiveGun == "Gun01")
        {
            guncooldown = 0.5f;
        }
        if (ActiveGun == "Gun02")
        {
            guncooldown = 0.4f;
        }
        if (ActiveGun == "Gun03")
        {
            guncooldown = 0.125f;
        }
        if (ActiveGun == "Gun04")
        {
            guncooldown = 0.45f;
        }
        if (ActiveGun == "Gun05")
        {
            guncooldown = 0.2f;
        }
        if (ActiveGun == "Gun06")
        {
            guncooldown = 0.3f;
        }
        if (ActiveGun == "Gun07")
        {
            guncooldown = 0.7f;
        }
        if (ActiveGun == "Gun08")
        {
            guncooldown = 0.075f;
        }
    }

    void shoot()
    {
        if (shootingcooldown == 0 && ActiveGun == "Gun00")
        {
            Instantiate(gun00Bullet, guns.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun00Bullet, guns.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun01")
        {
            Instantiate(gun01Bullet, guns.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun01Bullet, guns.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun02")
        {
            Instantiate(gun02Bullet, guns.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun02Bullet, guns.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun03")
        {
            Instantiate(gun03Bullet, guns.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun03Bullet, guns.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun04")
        {
            Instantiate(gun04Bullet, guns.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun04Bullet, guns.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun05")
        {
            Instantiate(gun05Bullet, guns.transform.GetChild(5).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun05Bullet, guns.transform.GetChild(5).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun06")
        {
            Instantiate(gun06Bullet, guns.transform.GetChild(6).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun06Bullet, guns.transform.GetChild(6).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun07")
        {
            Instantiate(gun07Bullet, guns.transform.GetChild(7).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun07Bullet, guns.transform.GetChild(7).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
        if (shootingcooldown == 0 && ActiveGun == "Gun08")
        {
            Instantiate(gun08Bullet, guns.transform.GetChild(8).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            Instantiate(gun08Bullet, guns.transform.GetChild(8).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
            shootingcooldown = guncooldown;
        }
    }
    #endregion
    #endregion
}