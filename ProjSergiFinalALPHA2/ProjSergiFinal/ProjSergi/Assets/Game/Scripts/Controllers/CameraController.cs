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
    public bool IsFollowing = true;
    public bool TypeCameraFixed;
    // Use this for initialization
    void Start()
    {
        IsFollowing = true;
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;// punt xy max
       // Water.transform.localScale)
    }

    // Update is called once per frame
    void Update()
    {

        //if (TypeCameraFixed)
        //{
        //    var x = transform.position.x;
        //    var z = transform.position.z;
        //    var y = transform.position.y;
        //    if (IsFollowing)
        //    {
        //        if (Mathf.Abs(x - Player.position.x) > Margin.x)
        //            x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
        //        if (Mathf.Abs(z - Player.position.z) > Margin.z)
        //            z = Mathf.Lerp(z, Player.position.z - 20f, Smoothing.z * Time.deltaTime);
        //        if (Mathf.Abs(y - Player.position.y) > Margin.y)
        //            y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
        //    }
        //    var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
        //    x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        //    z = Mathf.Clamp(z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);
        //    //transform.LookAt(Player.position);
        //    transform.position = new Vector3(x, y, z);
        //    // transform.position = new Vector3(Player.position.x, Player.position.y + 8.0f, Player.position.z -20.0f);
        //    transform.eulerAngles = Player.transform.rotation.eulerAngles;
        //    transform.LookAt(Player.position);

        //    //   transform.Rotate(new Vector3(0f,Player.rotation.eulerAngles.y,0f),Space.World);
        //    float halfWidth = Screen.width * 0.5f;
        //    float mouseXPos = Player.rotation.eulerAngles.y; 
        //    float differenceX = mouseXPos - halfWidth;
        //    float factorX = differenceX / halfWidth;
        //    //    transform.RotateAround(Player.transform.position, Vector3.up, Player.gameObject.G * Time.deltaTime);
        //    //    transform.RotateAround(Player.transform.position, Vector3.up, Player.rotation.eulerAngles.y);
        // //   transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);z

        //}
        //else
        //{
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
            //transform.LookAt(Player.position);
            transform.position = new Vector3(x, transform.position.y, z);
       // }
    }
}


