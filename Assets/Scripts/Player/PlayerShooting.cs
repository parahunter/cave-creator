using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public GameObject Flare;
    public float fireForce;

    public float timeDelay;
    private float timeSinceLast;


    void Update()
    {
        timeSinceLast += Time.deltaTime;

        if (timeSinceLast > timeDelay)
        {
            if (Input.GetButtonDown("FireFlare"))
            {
                GameObject NewFlare = Instantiate(Flare, transform.position, transform.rotation) as GameObject;
                NewFlare.rigidbody.AddForce(transform.forward * fireForce);
                timeSinceLast = 0;
            }
        }
    }

}
