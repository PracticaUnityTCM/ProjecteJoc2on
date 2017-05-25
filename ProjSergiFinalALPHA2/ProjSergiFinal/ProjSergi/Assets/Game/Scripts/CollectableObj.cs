using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeCollectableObj
{
    Health,
    Ammuntion,
    BigShipCharged,
    BulletFire
}
public class CollectableObj : MonoBehaviour {
    public float TimerActive;
    public float TimerActiveTotal;
    public float isActive;
	// Use this for initialization
	void Start () {
		
	}
    public TypeCollectableObj typeObj;

	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Ship")
        {
            if (typeObj == TypeCollectableObj.Ammuntion)
                GameManager.Instance.IncresaseAmunnition(10);
            else if (typeObj == TypeCollectableObj.Health)
                other.gameObject.GetComponent<ShipController>().Health += 10;
            else if (typeObj == TypeCollectableObj.BulletFire)
            {
                other.gameObject.GetComponent<ShipBulletsController>().UpdateBulletType(TypeBullet.Fire);
                //StartCoroutine(Countdown(10));
                //other.gameObject.GetComponent<ShipBulletsController>().UpdateBulletType(TypeBullet.Normal);
            }
            else if (typeObj == TypeCollectableObj.BigShipCharged)
            {
                //other.gameObject.GetComponent<ShipBulletsController>().SetBigChargerShip() ;
            }
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
  


private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
         //   Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Countdown Complete!");
    }

}
