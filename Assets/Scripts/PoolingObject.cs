using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    public static PoolingObject instance;
    public int amoutToPool;
    public GameObject objectPrefab;
    public bool shouldExpand;
    public List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreatePooling();
    }

    void CreatePooling()
    {
        for (int i = 0; i < amoutToPool; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            prefabs.Add(obj);
        }
    }

    public GameObject GetPoolingObject()
    {
        for(int i = 0; i < prefabs.Count; i++)
        {
            if(!prefabs[i].activeInHierarchy)
            {
                return prefabs[i];
            }
            
        }

        if (shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(objectPrefab);
            obj.SetActive(false);
            prefabs.Add(obj);
            return obj;
        }

        return null;
    }
}
