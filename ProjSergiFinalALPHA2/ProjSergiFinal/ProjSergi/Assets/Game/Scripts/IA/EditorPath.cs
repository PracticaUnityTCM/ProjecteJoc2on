using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour {
    public Color RayColor = Color.white;
    public List<Transform> pathObjs = new List<Transform>();

    Transform[] ArrayObjs;
    void OnDrawGizmos()
    {
        Gizmos.color = RayColor;
        ArrayObjs = GetComponentsInChildren<Transform>();
        pathObjs.Clear();
        foreach(Transform pathObj in ArrayObjs)
        {
            if(pathObj!=this.transform)
            {
                pathObjs.Add(pathObj);

            }
        }
        for (int i = 0; i < pathObjs.Count; i++)
        {
            Vector3 position = pathObjs[i].position;
            if(i>0)
            {
                Vector3 prev = pathObjs[i - 1].position;
                Gizmos.DrawLine(prev, position);
                Gizmos.DrawWireSphere(position, 0.3f);

            }
        }
    }
}
