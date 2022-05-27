using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemToPool
{
    public int amount;
    public GameObject item;
    public bool shouldExpand;
}

public class PoolingObject : MonoBehaviour
{
    public static PoolingObject instance;
    
    public List<itemToPool> itemToPoolList = new List<itemToPool>();
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
        foreach(itemToPool itemToPool in itemToPoolList)
        {
            for (int i = 0; i < itemToPool.amount; i++)
            {
                GameObject obj = Instantiate(itemToPool.item);
                obj.SetActive(false);
                prefabs.Add(obj);
            }
        }
    }

    public GameObject GetPoolingObject(string tagName)
    {
        for(int i = 0; i < prefabs.Count; i++)
        {
            if(!prefabs[i].activeInHierarchy && prefabs[i].tag == tagName)
            {
                return prefabs[i];
            }
            
        }

        foreach(itemToPool itemToPool in itemToPoolList)
        {
            if(itemToPool.item.tag == tagName)
            {
                if (itemToPool.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(itemToPool.item);
                    obj.SetActive(false);
                    prefabs.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}
