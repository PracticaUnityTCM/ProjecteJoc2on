using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq;
public class SpawnBullet : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	public void SpawnFrontBullet(Vector3 position,Quaternion rotation,TypeBullet type,float velocityForward,string Layer)
    {
        // create bullet
        GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        // get component Scrpit and Launch it
        bulletObj.GetComponent<BulletController>().SetTypeBullet(type);
        bulletObj.layer = LayerMask.NameToLayer(Layer);
        //bulletObj.GetComponent<BulletController>().FireW(rotation,position,velocityForward);
       
    }
    public void SpawnBulletFrontForce(Vector3 direction,TypeBullet type ,string Layer)
    {
        GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        // get component Scrpit and Launch it
        bulletObj.GetComponent<BulletController>().SetTypeBullet(type);
        bulletObj.layer = LayerMask.NameToLayer(Layer);
        bulletObj.GetComponent<BulletController>().ShootBulletForce(direction);
    }
    public void SpawnLateralBullets(Transform transformPSpawner,Quaternion rotation,bool isLeftSide,TypeBullet type,string Layer)
    {
        float angle = 70f;
        float num = 0;

        List<Transform> Child = transformPSpawner.GetComponentsInChildren<Transform>(true).Where(x => x.gameObject.transform.parent != transform.parent).ToList(); ;  
        foreach (Transform tr in Child)
        {

            Quaternion rotationLocal2 = Quaternion.Euler(new Vector3(tr.rotation.x, ((!isLeftSide) ? +(angle+num) : -(angle+num)), tr.rotation.z));
      
            GameObject bulletObj3 = Instantiate(bullet, new Vector3(tr.position.x, tr.position.y, tr.position.z), rotation) as GameObject;
            bulletObj3.layer = LayerMask.NameToLayer(Layer);
            bulletObj3.GetComponent<BulletController>().SetTypeBullet(type);    
            bulletObj3.GetComponent<BulletController>().Fire(tr.rotation, Vector3.zero);
            num += 20;
        }
        //Quaternion rotationLocal = Quaternion.Euler(new Vector3(rotation.x, rotation.y + ((isLeftSide) ? 110 : -110), rotation.z));
        //GameObject bulletObj = Instantiate(bullet, new Vector3(position.x,position.y,position.z-0.5f), rotationLocal) as GameObject;
        //bulletObj.GetComponent<BulletController>().Fire(rotationLocal);
        //Quaternion rotationLocal1 = Quaternion.Euler(new Vector3(rotation.x, rotation.y + ((isLeftSide) ? +95 : -95), rotation.z));
        //GameObject bulletObj2 = Instantiate(bullet, position,rotationLocal1) as GameObject;
        //bulletObj2.GetComponent<BulletController>().Fire(rotationLocal1);
        ////GameObject bulletObj2 = Instantiate(bullet, position, rotation) as GameObject;
        //*4 each in diferent angle of direccion

    }
    public void SpawnBulletToPosition(TypeBullet type,Vector3 tarjet,string Layer,bool isShootShip)
    {
        float velocity = 4f;
        GameObject bulletobj = Instantiate(bullet, transform.position, transform.rotation);
        bulletobj.GetComponent<BulletController>().SetTypeBullet(type);
        bulletobj.layer=LayerMask.NameToLayer(Layer);
       // bulletobj.GetComponent<BulletController>().ShootBulletToPosicionForce(tarjet, transform.position, 400f);
        bulletobj.GetComponent<BulletController>().FireToPosition(tarjet, transform.rotation,isShootShip,velocity);
    }
}

