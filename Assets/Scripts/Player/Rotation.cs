using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float rotationSpeed;
    public Vector3 Axis;

    void Update()
    {
        transform.Rotate(Axis, (rotationSpeed * Time.deltaTime));
    }


}
