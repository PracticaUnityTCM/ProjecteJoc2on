using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public Transform Player;
    public Vector3
        Margin,
        Smoothing;
    public BoxCollider Bounds;
    public GameObject Water;
    private Vector3
        _min,
        _max;
    public float offsetZ = 15f;
    public Vector3 RoitationCam;
    public bool IsFollowing = true;
    public bool TypeCameraFixed;
    public bool hasbeenFixedCam;
    // Use this for initialization
    void Start()
    {
        IsFollowing = true;
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;// punt xy max
       // Water.transform.localScale)
    }
    void Awake()
    {
        IsFollowing = true;
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
    }
    // Update is called once per frame
    void Update()
    {
        if (TypeCameraFixed)
        {
             hasbeenFixedCam = true;
             if(!Player.GetComponent<ShipController>().IsDeath)
                transform.position = Player.transform.position - Player.transform.forward * offsetZ+new Vector3(0,10f,0);
                transform.LookAt(Player.transform);
        }
        else
        {
            if (hasbeenFixedCam)
            {
                hasbeenFixedCam = false;
                transform.rotation = Quaternion.Euler(RoitationCam);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - offsetZ);
            }
            var x = transform.position.x;
            var z = transform.position.z;
            if (IsFollowing)
            {
                if (Mathf.Abs(x - Player.position.x) > Margin.x)
                    x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
                if (Mathf.Abs(z - Player.position.z) > Margin.z)
                    z = Mathf.Lerp(z, Player.position.z -offsetZ , Smoothing.z * Time.deltaTime);
            }
            var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
            x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            z = Mathf.Clamp(z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);
            transform.position = new Vector3(x, transform.position.y, z);
       }
    }
}


