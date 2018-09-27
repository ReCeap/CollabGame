using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    protected Vector2 direction;
    private Rigidbody2D rb;
    private float yAxis;
    public float maxVelocity = 3f;

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

    // Use this for initialization
    void Start () {
        //Makes a reference to the rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Shipmove(yAxis);
        faceMouse();
        vx = rb.velocity.x;
        vy = rb.velocity.y;

        yAxis = Input.GetAxis("Vertical");

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
}
