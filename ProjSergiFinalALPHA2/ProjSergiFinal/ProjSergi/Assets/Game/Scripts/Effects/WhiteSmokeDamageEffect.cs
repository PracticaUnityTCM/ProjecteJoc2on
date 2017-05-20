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

        if (health < 90) {
         //   Smoke.gameObject.SetActive(true);
            if (isShip)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0));
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, 0));
                }
            }
            else
            {
                if (enemyBehauvoir == EnemyShipBehaivour.Following)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else if (enemyBehauvoir == EnemyShipBehaivour.Patroling)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else if (enemyBehauvoir == EnemyShipBehaivour.Shooting)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, 0));
                }
            }
            UpdateSmoke(health);
        }

    }
    //public void UpdateSmokeEnemyBehaibour(EnemyShipBehaivour enemyBehauvoir,float heatlh)
    //{

    //     UpdateSmoke(heatlh);
    //}
    private void UpdateSmoke(float health)
    {
        Debug.Log(health);
        if (health < 80)
        {
            Debug.Log("health des");
            
            Smoke.Play();
        }
        if (health < 50)
        {
           // Light.gameObject.SetActive(true);
            //Guspires.gameObject.SetActive(true);
            Guspires.Play();
        }
        var em = Smoke.emission;
        float num = Mathf.Abs(health - 100);
        Debug.Log("ss"); ;
        em.rateOverTime = num;

    }
}



