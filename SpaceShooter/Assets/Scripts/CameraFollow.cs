using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;

    public deathHandler deathCheck;

    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Handler");
        deathCheck = g.GetComponent<deathHandler>();
    }

    void LateUpdate ()
    {
        if (deathCheck.death == false)
        {
            transform.position = target.position + offset;
        }
    }
}
