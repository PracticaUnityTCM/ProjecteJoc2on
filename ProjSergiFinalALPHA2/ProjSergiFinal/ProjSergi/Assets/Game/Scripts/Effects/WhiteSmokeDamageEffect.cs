using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSmokeDamageEffect : MonoBehaviour
{
    private ParticleSystem Smoke;
    private Light Light;
    private ParticleSystem Guspires;
    private float HealthLoc;
    void Start()
    {
        Light = GetComponentInChildren<Light>();
        Light.gameObject.SetActive(false);
        Guspires = GetComponentInChildren<ParticleSystem>();
        Guspires.gameObject.SetActive(false);
        Smoke = GetComponent<ParticleSystem>();
        Smoke.gameObject.SetActive(false);
    }
    public void UpdateSmoke(float health, bool isShip, EnemyShipBehaivour enemyBehauvoir)
    {

        if (health < 90f) { 
            Smoke.gameObject.SetActive(true);
            if (isShip)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0));
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(225f, 0, 0));
                }
                else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(-45f, 0, 0));
                }
                else
                    transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, 0));
            }
            else
            {
                if(enemyBehauvoir==EnemyShipBehaivour.Turret)
                {

                }
                if (enemyBehauvoir == EnemyShipBehaivour.Following)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(225, 0, 0));
                }
                else if (enemyBehauvoir == EnemyShipBehaivour.Patroling)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(225, 0, 0));
                }
                else if (enemyBehauvoir == EnemyShipBehaivour.Shooting)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(-45f, 0, 0));
                }
            }
            UpdateSmoke(health);
        }

    }
    private void UpdateSmoke(float health)
    {
        Debug.Log(health);
        if (health < 80f)
        {            
            Smoke.Play();
        }
        if (health < 50f)
        {
            Light.gameObject.SetActive(true);
            Guspires.gameObject.SetActive(true);
            Guspires.Play();
        }
        var em = Smoke.emission;
        float num = Mathf.Abs(health - 100);
        num = num / 20.0f;
        em.rateOverTime = num;

    }
}



