using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
class KeysManager : MonoBehaviour
{

    public static KeysManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

