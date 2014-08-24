using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject Engine1;
    public GameObject Engine2;
    public Light EngineLight;

    private Rotation Engine1Rot;
    private Rotation Engine2Rot;
    private ParticleSystem Engine1PS;
    private ParticleSystem Engine2PS;

    
    public float forwardSpeedCoef;
    public float sideSpeedCoef;
    public float upSpeedCoef;

    public float ABSpeedMod;

    private float forwardSpeedOri;
    private float engRotSpeedOri;
    private float engLightOri;
    private float engPSEmissionOri;
    private float engPSSpeedOri;

    void Start()
    {
        Engine1Rot = Engine1.GetComponent<Rotation>();
        Engine2Rot = Engine2.GetComponent<Rotation>();
        Engine1PS = Engine1.gameObject.transform.FindChild("Particle System").GetComponent<ParticleSystem>();
        Engine2PS = Engine2.gameObject.transform.FindChild("Particle System").GetComponent<ParticleSystem>();

        engRotSpeedOri = Engine1Rot.rotationSpeed;
        forwardSpeedOri = forwardSpeedCoef;
        engLightOri = EngineLight.intensity;
        engPSSpeedOri = Engine1PS.startSpeed;
        engPSEmissionOri = Engine1PS.emissionRate;

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
        float maxRotSpeed = engRotSpeedOri * 6;
        float maxLightIntensity = engLightOri * 6;
        float maxPSEmission = engPSEmissionOri * 15;
        float maxPSSpeed = engPSSpeedOri * 6;

        while (true)
        {
            float fwThrust = Input.GetAxis("ForwardThrust");

            Engine1Rot.rotationSpeed = Mathf.Lerp(engRotSpeedOri, maxRotSpeed, fwThrust);
            Engine2Rot.rotationSpeed = Mathf.Lerp(-engRotSpeedOri, -maxRotSpeed, fwThrust);
            EngineLight.intensity = Mathf.Lerp(engLightOri, maxLightIntensity, fwThrust);

            if (fwThrust > 0.05)
            {
                Engine1PS.startSpeed = maxPSSpeed;
                Engine1PS.emissionRate = maxPSEmission;
                Engine2PS.startSpeed = maxPSSpeed;
                Engine2PS.emissionRate = maxPSEmission;
            }
            else
            {
                Engine1PS.startSpeed = engPSSpeedOri;
                Engine1PS.emissionRate = engPSEmissionOri;
                Engine2PS.startSpeed = engPSSpeedOri;
                Engine2PS.emissionRate = engPSEmissionOri;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
