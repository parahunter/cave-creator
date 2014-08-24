using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    
    public float forwardSpeedCoef;
    public float sideSpeedCoef;
    public float upSpeedCoef;

    private float forwardSpeedOri;


    void Start()
    {
        forwardSpeedOri = forwardSpeedCoef;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Afterburner"))
            forwardSpeedCoef = forwardSpeedOri * 2;
        else forwardSpeedCoef = forwardSpeedOri;

        Vector3 MoveDir = (transform.forward * Input.GetAxis("ForwardThrust") * forwardSpeedCoef * Time.deltaTime)
                         + (transform.right * Input.GetAxis("SideThrust") * sideSpeedCoef * Time.deltaTime)
                         + (transform.up * Input.GetAxis("UpThrust") * upSpeedCoef * Time.deltaTime);
        rigidbody.AddForce(MoveDir);
    }


}
