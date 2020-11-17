using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<ObjectPool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    #region Singleton
    public static PoolManager Instance;

    private void Awake()
    {
        Instance = this;

        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(ObjectPool pool in Pools)
        {
            // Pool objects
            Queue<GameObject> queue = new Queue<GameObject>();

            for(int i = 0; i < pool.Size; i++)
            {
                GameObject item = Instantiate(pool.Prefab);
                item.SetActive(false);

                queue.Enqueue(item);
            }

            PoolDictionary.Add(pool.Tag, queue);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if(!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Key " + tag + " does not exist in PoolDictionary.");
            return null;
        }
        
        GameObject spawned = PoolDictionary[tag].Dequeue();

        spawned.SetActive(true);
        spawned.transform.position = position;
        spawned.transform.rotation = rotation;

        IPoolable poolable = spawned.GetComponent<IPoolable>();
        poolable?.OnSpawn();

        PoolDictionary[tag].Enqueue(spawned);

        return spawned;
    }
}
