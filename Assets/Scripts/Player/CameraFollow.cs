using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private GameObject PlayerCtrl;
    public Vector3 PlayerPos;

    public float Distance;
    public float Angle;

    public float ScrollLimitLower;
    public float ScrollLimitUpper;


    void Start()
    {
        PlayerCtrl = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        PlayerCtrl = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetAxis("ScrollWheel") < 0)
            Distance += 0.1f;
        else if (Input.GetAxis("ScrollWheel") > 0)
            Distance -= 0.1f;

        if (Distance < ScrollLimitLower) Distance = ScrollLimitLower;
        else if (Distance > ScrollLimitUpper) Distance = ScrollLimitUpper;

        PlayerPos = PlayerCtrl.transform.localPosition;
        PlayerPos.x += 0 * Distance;
        PlayerPos.y += 14 * Distance;
        PlayerPos.z += Angle * Distance;

        transform.position = PlayerPos;
        transform.LookAt(PlayerCtrl.transform);
    }

}
