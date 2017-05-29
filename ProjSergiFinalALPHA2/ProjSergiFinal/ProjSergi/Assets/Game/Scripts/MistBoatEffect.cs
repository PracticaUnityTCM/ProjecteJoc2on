using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistBoatEffect : MonoBehaviour {
    ParticleSystem PS;
	// Use this for initialization
	void Start () {
        PS = GetComponentInChildren< ParticleSystem>();
	}
	public void UpdateMistBoatEffect(float velocit)
    {
        if(velocit==0.0f)
        {
            velocit = 0;
        }
        
            var ForceOverLifeTimeMod = PS.velocityOverLifetime;
            ForceOverLifeTimeMod.z = velocit;

        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
