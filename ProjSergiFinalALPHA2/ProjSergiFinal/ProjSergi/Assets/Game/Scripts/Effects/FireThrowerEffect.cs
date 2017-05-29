using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireThrowerEffect : MonoBehaviour
{
    public ParticleSystem[] particles;
    public bool start;
    // Use this for initialization
    void Start()
    {
        particles = gameObject.GetComponentsInChildren<ParticleSystem>();
    }
    public void StartFireEffect()
    {
        start = true;
        foreach (ParticleSystem p in particles)
        {
            p.Play();
            Debug.Log(p+"pp");
        }
       
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
    }
    public void Stop()
    {
        start = false;
        foreach (ParticleSystem p in particles)
        {
            p.Stop();
        }
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
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

