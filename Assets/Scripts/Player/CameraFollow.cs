using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform PlayerTran;
    private Transform CamFollowPoint;

    public float followSpeed;
    public float Distance;
    public float Angle;

    public float ScrollLimitLower;
    public float ScrollLimitUpper;

    private Vector3 PlayerPos;
    private Vector3 PrevPlayerPos;


    void Start()
    {
        PlayerTran = GameObject.FindGameObjectWithTag("Player").transform;
        CamFollowPoint = PlayerTran.FindChild("CameraFollowPoint");
    }

    void FixedUpdate()
    {
        PlayerPos = CamFollowPoint.position;

        float rate = followSpeed / Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, PlayerPos, rate);

        transform.LookAt(PlayerTran);
    }

}
