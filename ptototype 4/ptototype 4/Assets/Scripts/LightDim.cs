using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDim : MonoBehaviour
{
    public GameObject pointLight;
    Light lightIntensity;
    private bool test = false;


    // Start is called before the first frame update
    void Start()
    {
        lightIntensity = pointLight.GetComponent<Light>();
        //while this references the entire Light component, it is only being used for the intensity of the pointlight
        StartCoroutine(dimLight());


    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(dimLight());


    }
    IEnumerator dimLight()
    {
        while(true)
        {
            
                while(lightIntensity.intensity > 2f)
                {
                    yield return new WaitForSeconds(0.3f);
                    lightIntensity.intensity -= 0.1f;
                }
                test = false;
            while(lightIntensity.intensity < 10)
            {
                {
                    yield return new WaitForSeconds(0.3f);
                    lightIntensity.intensity += 0.1f;
                }

            }
        }
        
    }
}
