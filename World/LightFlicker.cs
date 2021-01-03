using System.Collections;
using UnityEngine;

namespace RPG.World
{
public class LightFlicker : MonoBehaviour
    {
        Light light;
        public bool isFlickering;
        public float flickeringIntensityMin = 1.1f;
        public float flickeringIntensityMax = 2.3f;
        float flickeringIntensity;
        float timeDelay;
        public float timeDelayMin = 0.3f;
        public float timeDelayMax = 0.65f;
        void Start() 
        {
            light = GetComponent<Light>();
        }
        void Update()
        {
            if(isFlickering == false)
            {
                StartCoroutine(FlickeringLight());
            }
        }

        IEnumerator FlickeringLight()
        {
            isFlickering = true;
            flickeringIntensity = Random.Range(flickeringIntensityMin,flickeringIntensityMax);
            light.intensity = flickeringIntensity;
           
            timeDelay = Random.Range(timeDelayMin, timeDelayMax);
            yield return new WaitForSeconds(timeDelay);

            flickeringIntensity = Random.Range(flickeringIntensityMin, flickeringIntensityMax);
            light.intensity = flickeringIntensity;

            timeDelay = Random.Range(timeDelayMin, timeDelayMax);
            yield return new WaitForSeconds(timeDelay);
            
            isFlickering = false;
        }
    }

}
