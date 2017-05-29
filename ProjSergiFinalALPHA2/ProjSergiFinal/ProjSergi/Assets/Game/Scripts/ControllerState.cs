using System;
using UnityEngine;
[System.Serializable]
public class ControllerState{ 
    [SerializeField]
    public bool isCollidingFront { get; set; }
    public bool isCollidingBack { get; set; }
    public bool isCollidingLeft { get; set; }
    public bool isCollidingRight { get; set; }
    public bool isFiring { get; set; }
    public bool HasCollisions { get { return isCollidingBack || isCollidingFront || isCollidingLeft || isCollidingRight; } }
    public void Reset()
    {
        isCollidingRight = isCollidingLeft = isCollidingBack = isCollidingFront = isFiring = false;

    }
    public override string ToString()
    {
        return string.Format("(controller: right:{0} left:{1} front:{2} back:{3} Fire:{4})",
              isCollidingRight,
              isCollidingLeft,
              isCollidingFront,
              isCollidingBack,
              isFiring
              );
    }
}
