using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeCollectableObj
{
    Health,
    Ammuntion,
    BigShipCharged,
    BulletFire,
    FireThrower

}
public class CollectableObj : MonoBehaviour {
	// Use this for initialization
	void Start () { 
	}
    public TypeCollectableObj typeObj;

	// Update is called once per frame
	void Update () {
        if (DestroyObj)
            StartCoroutine(ScaleDestroy());
        // StartCoroutine(ScaleDestroy());
    }
    public bool DestroyObj = false;
    void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.tag=="Ship")
        {
            if (typeObj == TypeCollectableObj.Ammuntion)
            {
                GameManager.Instance.IncresaseAmunnition(10);
                DestroyObj = true;
            }
            else if (typeObj == TypeCollectableObj.Health)
            {
                if (other.gameObject.GetComponent<ShipController>().Health < 90)
                {
                    other.gameObject.GetComponent<ShipController>().Health += 10;
                    DestroyObj = true;
                }
            }
            else if (typeObj == TypeCollectableObj.BulletFire)
            {
                DestroyObj = true;
                other.gameObject.GetComponent<ShipBulletsController>().SetTypeBullet(TypeBullet.Fire);
            }
            else if (typeObj == TypeCollectableObj.BigShipCharged)
            {
                other.gameObject.GetComponent<ShipBulletsController>().SetBigChargerShip() ;
                DestroyObj = true;
            }
            else if (typeObj == TypeCollectableObj.FireThrower)
            {
                DestroyObj = true;
                other.gameObject.GetComponent<ShipBulletsController>().powerUpFireThrowerActive = true;
            }
            
        }
    }
    IEnumerator ScaleDestroy()
    {
       // yield return null;           
        transform.Rotate(0, 1, 0);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 0.5f*Time.deltaTime);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }



}
