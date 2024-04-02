using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPatten : MonoBehaviour
{
    private static SingletonPatten instance;

    public static SingletonPatten Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SingletonPatten>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<SingletonPatten>();
                    instance = newObj;
                }
            }
            return instance;
        }         
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<SingletonPatten>();
        if(objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
