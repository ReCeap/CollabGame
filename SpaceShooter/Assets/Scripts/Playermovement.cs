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

        //yAxis = Input.GetAxis("Vertical");

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
        if (Input.GetInput(KeyCode.W))
            {
            Vector2 force = transform.up * amount;

            rb.AddForce(force);
            }
    }
}
