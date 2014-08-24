using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rotation Engine1Rot;
    public Rotation Engine2Rot;
    public Light EngineLight;

    
    public float forwardSpeedCoef;
    public float sideSpeedCoef;
    public float upSpeedCoef;

    public float ABSpeedMod;

    private float forwardSpeedOri;
    private float engRotSpeedOri;
    private float engLightOri;

    void Start()
    {
        forwardSpeedOri = forwardSpeedCoef;
        engRotSpeedOri = Engine1Rot.rotationSpeed;
        engLightOri = EngineLight.intensity;

        StartCoroutine(ChangeEnginesDuringThrust());
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Afterburner"))
            forwardSpeedCoef = forwardSpeedOri * ABSpeedMod;
        else forwardSpeedCoef = forwardSpeedOri;

        Vector3 MoveDir = (transform.forward * Input.GetAxis("ForwardThrust") * forwardSpeedCoef * Time.deltaTime)
                         + (transform.right * Input.GetAxis("SideThrust") * sideSpeedCoef * Time.deltaTime)
                         + (transform.up * Input.GetAxis("UpThrust") * upSpeedCoef * Time.deltaTime);
        rigidbody.AddForce(MoveDir);
    }



    private IEnumerator ChangeEnginesDuringThrust()
    {
        float maxRotSpeed = engRotSpeedOri * 5;
        float maxLightIntensity = engLightOri * 6;

        while (true)
        {
            float fwThrust = Input.GetAxis("ForwardThrust");

            Engine1Rot.rotationSpeed = Mathf.Lerp(engRotSpeedOri, maxRotSpeed, fwThrust);
            Engine2Rot.rotationSpeed = Mathf.Lerp(-engRotSpeedOri, -maxRotSpeed, fwThrust);
            EngineLight.intensity = Mathf.Lerp(engLightOri, maxLightIntensity, fwThrust);
            yield return new WaitForEndOfFrame();
        }
    }
}
