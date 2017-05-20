using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExploson : MonoBehaviour {
    private ParticleSystem p;
    private ParticleSystem[] child;
    public void StartEffectExploson()
    {
        p = GetComponent<ParticleSystem>();
        child = GetComponentsInChildren<ParticleSystem>();
        p.Play();
        foreach(ParticleSystem p in child)
        {
           if(EffectsManager.Instance.EnableEffects)
           p.Play();
        }
        StartCoroutine("Wait", 2f);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
