using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPool Instance { get; private set; }

	void Awake ()
    {
        Instance = this;  
	}

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                poolDictionary[pool.tag].Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool doesn't exist");
            return null;
        }

        var instance = poolDictionary[tag].Dequeue();
        instance.SetActive(true);
        instance.transform.position = position;
        instance.transform.rotation = rotation;

        IPoolObject poolObj = instance.GetComponent<IPoolObject>();

        if (poolObj != null)
        {
            poolObj.OnSpawn();
        }

        poolDictionary[tag].Enqueue(instance);
        return instance;
    }
   
}
