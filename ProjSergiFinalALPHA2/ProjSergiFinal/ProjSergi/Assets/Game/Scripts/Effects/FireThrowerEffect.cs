using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class FireThrowerEffect : MonoBehaviour {
    private AudioSource AS;
    public ParticleSystem[] particles;
        public bool start;
        // Use this for initialization
        void Start() {
            start = false;
        particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        }
        public void StartFireEffect()
        {
            start = true;
            foreach (ParticleSystem p in particles) {
                p.Play();
            }
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
        }
        public void Stop()
        {
            start = false;
            foreach (ParticleSystem p in particles)
            {
                p.Stop();
            }
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(2).GetComponent<BoxCollider>().enabled=false;
        }
        // Update is called once per frame
        void Update() {
            if (start)
            {
                foreach (ParticleSystem p in particles)
                {
                    ParticleSystem.EmissionModule ParticleEm = p.emission;
                    ParticleEm.enabled = true;   
                }
            }
            else
            {
                foreach (ParticleSystem p in particles)
                {
                    ParticleSystem.EmissionModule ParticleEm = p.emission;
                    ParticleEm.enabled = false;
                }
            }
        }
    }

