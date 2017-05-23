using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;


public class SmokeShotEffect : MonoBehaviour {
    private ParticleSystem particules;
	// Use this for initialization
	void Start () {
        particules = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartSmokeShotEffect()
    {
        if(particules!=null)
        {
                particules.Play();
        }
        StartCoroutine("WaitAndDestroy", 2f);
    }
    public IEnumerator WaitAndDestroy(float num)
    {
        yield return new WaitForSeconds(num);
        Destroy(gameObject);
    }
}

