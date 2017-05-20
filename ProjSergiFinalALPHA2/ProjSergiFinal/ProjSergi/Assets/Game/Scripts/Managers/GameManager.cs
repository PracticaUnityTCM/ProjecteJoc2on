using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool EnableEffects;
    // Use this for initialization
    private static GameManager _instance = null;
    //  public static AudioManager Instance { get { return instance; } }
  
    public ShipController Ship;

   
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
            Ship = GameObject.Find("Ship (1)").GetComponent<ShipController>();
            Ship.Health = 100;
            Ship.amunnition = 15;
        }
    }
    void Start () {
        Ship = GameObject.Find("Ship (1)").GetComponent<ShipController>();
       
       Ship.Health = 100;
       Ship.amunnition = 15;
	}
	public void DescreseAmuntion(int num)
    {
       Ship.amunnition -= num;
    }
    public void IncresaseAmunnition(int num)
    {
        Ship.amunnition += num;
    }
	// Update is called once per frame
	void Update () {
		
	}
    public bool CanShootNumBullets(int num)
    {
       if (Ship.amunnition < num)
            return false;
        return true;
    }
}
