using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullsEyeTrigger : MonoBehaviour {
    public GameObject Door;
    public List<GameObject> EnemiesToSpawn;
    public float moveSpeed=100f;
    public Vector3 direcction;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.tag == "Bullet")
        {
            Debug.Log("ss");
         //   float step = moveSpeed * Time.deltaTime;
            Door.GetComponent<Rigidbody>().AddForce(direcction * moveSpeed);
            if(EnemiesToSpawn.Count>0)
            foreach (GameObject obj in EnemiesToSpawn)
            {
                obj.SetActive(true);
            }
            // Door.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            // Door.transform.position = Vector3.Lerp(Door.transform.position,endPos, step);
            //Door.transform.Translate(new Vector3(-150f , 0, 0) * speed * Time.deltaTime);
            StartCoroutine(MoveDooor());
        }
    }
    IEnumerator MoveDooor()
    {
        yield return new WaitForSeconds(2.5f);
        Door.GetComponent<Rigidbody>().isKinematic = true; 

    }
}
