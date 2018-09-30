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
    public float maxVelocity = 3f;

    public string ActiveGun;

    public GameObject guns;

    public float vx;
    public float vy;
    public float fvx;
    public float fvy;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
    #region BulletGameObjects

    public GameObject gun00Bullet;

    #endregion


    #endregion

    #region Start&Update
    // Use this for initialization
    void Start () {
        //Makes a reference to the rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        pl = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Shipmove(yAxis);
        faceMouse();
        ShootingInput();
        vx = rb.velocity.x;
        vy = rb.velocity.y;

        yAxis = Input.GetAxis("Vertical");

    }
    #endregion

#region Voids
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

    void ShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(ActiveGun == "Gun00")
            {
                gun00();
            }
        }
        if (Input.GetKey(KeyCode.F))
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
    }

    void gun00()
    {
        Instantiate(gun00Bullet, guns.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.transform.position, pl.transform.rotation);
        Instantiate(gun00Bullet, guns.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.transform.position, pl.transform.rotation);
    }
    #endregion
}