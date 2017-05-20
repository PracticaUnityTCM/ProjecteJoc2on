using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SplashWaterEffect : MonoBehaviour
{ 
        private ParticleSystem[] particulas;
        // Use this for initialization
        void Start()
        {

 
            
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        public void StartWaterSplash()
        {
            particulas = GetComponentsInChildren<ParticleSystem>();
            if (particulas != null)
            {
                foreach (ParticleSystem p in particulas)
                {
                  
                    p.Play();
                  
                }
                StartCoroutine("Wait");
            }
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }

