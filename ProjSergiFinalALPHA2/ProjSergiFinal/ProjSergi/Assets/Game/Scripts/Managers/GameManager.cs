using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool EnableEffects;
    // Use this for initialization
    private static GameManager _instance = null;
    //  public static AudioManager Instance { get { return instance; } }
  
    public GameObject Ship;

   
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        
            Ship = GameObject.Find("Ship");
            Debug.Log(Ship);
        }
        
        Ship.GetComponent<ShipController>().Health = 100;
        Ship.GetComponent<ShipController>().amunnition = 15;
    }
    void Start () {
      // Ship = GameObject.Find("Ship(1)").GetComponent<ShipController>();
       
       Ship.GetComponent<ShipController>().Health = 100;
       Ship.GetComponent<ShipController>().amunnition = 15;
	}
	public void DescreseAmuntion(int num)
    {
       Ship.GetComponent<ShipController>().amunnition -= num;
    }
    public void IncresaseAmunnition(int num)
    {
        
        Ship.GetComponent<ShipController>().amunnition += num;
    }
	// Update is called once per frame
	void Update () {
		
	}
    
}
