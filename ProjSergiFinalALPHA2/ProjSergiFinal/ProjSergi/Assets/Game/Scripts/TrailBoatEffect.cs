using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBoatEffect : MonoBehaviour {
    public ParticleSystem PS;
    public float forceOverLifeTime;
    
	// Use this for initialization
	void Start () {
        PS=GetComponent<ParticleSystem>();
        gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void UpdateParticleSystemTrail(float velocitat)
    {
        var em = PS.emission;
    //    Debug.Log(velocitat+"velocitar foam no local");
        if (velocitat < 0.0f)
            velocitat = Mathf.Abs(velocitat);
        em.rateOverTime = velocitat * 1f;
        
    }
    public void UpdateParticleSystemTrailForceOverLifeTime(float velocity)
    {
        ParticleSystem.ForceOverLifetimeModule em = PS.forceOverLifetime;

        em.x = velocity * 2f;
    }
}
