using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward, (rotationSpeed * Time.deltaTime));
    }


}
