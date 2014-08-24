using UnityEngine;
using System.Collections;

public class FlareLight : MonoBehaviour {

    public float timeTilOut;

    private Light light;
    private float rangeOri;

    void Awake()
    {
        light = GetComponent<Light>();
        rangeOri = light.range;
        StartCoroutine(AttenuateLight());
    }


    private IEnumerator AttenuateLight()
    {
        yield return new WaitForSeconds(timeTilOut / 2);

        float rate = 1 / (timeTilOut / 2);
        float i = 1;
        
        while (i > 0)
        {
            i -= Time.deltaTime * rate;
            light.range = Mathf.Lerp(0, rangeOri, i);
            yield return new WaitForEndOfFrame();
        }

        light.enabled = false;
    }
}
