using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAspectRatioScale : MonoBehaviour {
    public Vector2 scaleOnRatio = new Vector2(0.1f, 0.1f);
    private Transform myTrans;
    private float withHeightRatio;
	// Use this for initialization
	void Start () {
        myTrans = transform;
        SetScale();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void SetScale()
    {
        withHeightRatio = (float)Screen.width / Screen.height;
        myTrans.localScale = new Vector3(scaleOnRatio.x, withHeightRatio * scaleOnRatio.y, 1);
    }
}
