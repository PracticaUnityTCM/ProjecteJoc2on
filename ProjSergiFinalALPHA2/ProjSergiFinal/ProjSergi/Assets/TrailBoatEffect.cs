using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBoatEffect : MonoBehaviour {
    public ParticleSystem PS;
    private float forceOverLifeTime;
	// Use this for initialization
	void Start () {
        PS=GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        var forceOverLifteTimeModule = PS.forceOverLifetime;
        forceOverLifteTimeModule.x = forceOverLifeTime;
	}
    public void UpdateParticleSystemTrail(float velocitat)
    {
        forceOverLifeTime = velocitat;
    }
}
