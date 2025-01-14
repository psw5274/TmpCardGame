﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class can be named as 'public class Singleton'
public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _ShuttingDown = false;
    private static object _Lock = new object();
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }
            lock (_Lock)
            {
                if (_Instance == null)
                {   
                    _Instance = (T)FindObjectOfType(typeof(T));
                    if (_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        _Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                    
                }
                return _Instance;
            }
        }
        set { _Instance = value; }
    }        
}
