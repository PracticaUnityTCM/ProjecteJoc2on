using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers {
    public static class Helpers  {
        public static void Parent(GameObject parent,GameObject child )
        {
            child.transform.parent = parent.transform;
        }
        public static Transform FindDeepChild   (this Transform aParent, string aName)
        {
            var result = aParent.Find(aName);
            if (result != null)
                return result;
            foreach (Transform child in aParent)
            {
                result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }  
        public static IEnumerator WaitIE(float num)
        { 
            yield return new WaitForSeconds(num);
        }
    }
  

}
