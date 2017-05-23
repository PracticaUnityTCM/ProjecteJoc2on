using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeThrowerEffect : MonoBehaviour {
    private ParticleSystem particles;
	// Use this for initialization
	void Start () {
      
    }
    void Awake()
    { 
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
           // Debug.Log("Start null particl4s");
        }
    }
    // Update is called once per frame
    void Update() {
    }
    public void StartSmokeEffect()
    {
        if (particles != null)
        {   
            particles.Play();
        }
       // StartCoroutine("WaitAndDestroy", 5f);
    }
    public IEnumerator WaitAndDestroy(float num)
    {
        yield return new WaitForSeconds(num);
        Destroy(gameObject);
    }


}
