using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling:MonoBehaviour {

    Dictionary<string, PoolInstance> cache;

    private void Awake()
    {
        cache = new Dictionary<string, PoolInstance>();
        StartCoroutine(SlowCleanupOfEmptyValues());
    }

    // pool and destroy every child.
    public void DestroyPooledObject(string group, GameObject obj, BasicMono settings)
    {
        AllAsStandby(obj.GetComponentsInChildren<IPooling>());
        if (PoolExists(group))
        {
            cache[group].DestroyExisting(obj);
        }
        obj.transform.position = new Vector2(10000, 0);
        // ignore incorrect tags
        Debug.Log("Tag doesn't exist. Won't be pooled, just moved and .ipooling called.."+ group);
    }

    public GameObject CreateInstance(
        string registerUnderTag, 
        Transform prefab, 
        Vector2 pos, 
        Quaternion rotation)
    {
        Debug.Log("Create instance under tag "+registerUnderTag);
        if (!PoolExists(registerUnderTag))
        {
            CreateNewPoolType(10, registerUnderTag, prefab, true);
        }
        GameObject obj = cache[registerUnderTag].CreateFromPool(pos, rotation);
        
        AllAsReady(obj.GetComponentsInChildren<IPooling>());
        return obj;
    }

    void AllAsReady(IPooling[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnPooledReady();
        }
    }

    void AllAsStandby(IPooling[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnPooledStandby();
        }
    }
    private void CreateNewPoolType(int startSize, string group, Transform prefab, bool dynamic)
    {
        Debug.Log("Create pool type with tag "+group);
        PoolInstance instance = new PoolInstance(group, prefab, startSize, dynamic); 
        cache.Add(group, instance);
    }
    
    private bool PoolExists(string registerTag)
    {
        return registerTag != null && cache.ContainsKey(registerTag);
    }

    private IEnumerator SlowCleanupOfEmptyValues()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            foreach (var pool in cache.Values)
            {
                pool.Cleanup();
                yield return null;
            }
        }
    }
    private class PoolItem {
        public GameObject instance;
        public bool isInPoolOnStandby = true;

        // object is disabled until recalled to next pool.
        internal void SetToStandby()
        {
            isInPoolOnStandby = true;
            instance.transform.position = Vector2.one* 100000;
        }

        internal void SetToReady(Vector2 pos)
        {
            isInPoolOnStandby = false;
            instance.transform.position = pos;
        }

        internal void SetToReady(Vector2 pos, Quaternion rot)
        {
            isInPoolOnStandby = false;
            instance.transform.localPosition = pos;
            instance.transform.localRotation = rot;
            //instance.GetComponent<IPoolable>().Pool();
        }

        internal void SetToReady(Quaternion rot)
        {
            isInPoolOnStandby = false;
            instance.transform.localRotation = rot;
        }
    }

    private class PoolInstance {
        string group;
        Transform prefab;
        bool isSizeDynamic;
        int startSize;
        List<PoolItem> createdObjects;

        public PoolInstance(string group, Transform prefab, int startSize, bool dynamicSize = true)
        {
            createdObjects = new List<PoolItem>();
            this.group = group;
            this.prefab = prefab;
            this.startSize = startSize;// todo
            this.isSizeDynamic = dynamicSize;
        }

        /// <summary>
        /// Use sparringly. It's safer to use create.
        /// </summary>
        /// <param name="obj"></param>
        public void AddExistingToPool(GameObject obj)
        {
            PoolItem item = new PoolItem() { instance = obj, isInPoolOnStandby = false };
            //item.SetToReady(); no position information.
            createdObjects.Add(item);
        }

        internal void DestroyExisting(GameObject obj)
        {
            if(obj == null)
            {
                Debug.Log("Pool-destroyed object is null. Skipped.");
                return;
            }
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] != null &&
                    createdObjects[i].instance == obj)
                {
                    Debug.Log("Pool-Standby existing, id:"+i+createdObjects[i].instance);
                    createdObjects[i].SetToStandby();
                    return;
                }
            }
            Debug.Log("Couldn't destroy object because it's not in pool, adding it to pool on standby. "+obj, obj);
            PoolItem item = new PoolItem() { instance = obj, isInPoolOnStandby = true };
            item.SetToStandby();
            createdObjects.Add(item);
        }

        public void Cleanup()
        {
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] == null)
                {
                    createdObjects.RemoveAt(i);
                    i--;
                }
            }
        }

        public bool PoolHasObjectOnStandby()
        {
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] != null &&
                    createdObjects[i].isInPoolOnStandby)
                    return true;
            }
            return false;
        }
        public GameObject CreateFromPool(Vector2 pos)
        {
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] != null &&
                    createdObjects[i].isInPoolOnStandby)
                {
                    createdObjects[i].SetToReady(pos, Quaternion.identity);
                    return createdObjects[i].instance;
                }
            }
            GameObject newInstance = Instantiate(prefab, pos, Quaternion.identity).gameObject;
            AddExistingToPool(newInstance);
            return newInstance;
        }
        public GameObject CreateFromPool(Vector2 pos, Quaternion rotation)
        {
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] != null &&
                    createdObjects[i].isInPoolOnStandby)
                {
                    createdObjects[i].SetToReady(pos, rotation);
                    return createdObjects[i].instance;
                }
            }
            GameObject newInstance = Instantiate(prefab, pos, rotation).gameObject;
            AddExistingToPool(newInstance);
            return newInstance;
        }
    }

}