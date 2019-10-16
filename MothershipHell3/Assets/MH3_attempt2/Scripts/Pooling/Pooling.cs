using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Pooling : MonoBehaviour {

    Dictionary<string, PoolInstance> cache;

    List<DebugMessage> debugMessages;

    public ReadOnlyDictionary<string, PoolInstance> Cache { get => new ReadOnlyDictionary<string, PoolInstance>(cache); }

    public ReadOnlyCollection<DebugMessage> DebugMessages { get => debugMessages.AsReadOnly(); }

    bool updatePoolingEditor;

    private void Awake()
    {
        cache = new Dictionary<string, PoolInstance>();
        debugMessages = new List<DebugMessage>();
        StartCoroutine(SlowCleanupOfEmptyValues());
    }

    // pool and destroy every child.
    public bool DestroyPooledObject(string group, GameObject obj, BasicMono settings)
    {
        // object can be poolable, but can't be pooled because group is missing.

        if (PoolExists(group))
        {
            CallbackStandby(obj.GetComponentsInChildren<IPooling>());
            if (obj == null)
            {
                PoolingDebug("Pool-destroyed object is null. Skipped.");
            }
            bool isInPool = cache[group].FindAndMarkAsStandby(obj);
            if (!isInPool)
            {
                PoolItem newPoolItem = cache[group].AddExistingToPool(obj);
                newPoolItem.MarkAsStandby();
                PoolingDebug("Adding new object to pool after it was destroyed. "+group+"|" + obj, obj);
            }
            else
            {
                PoolingDebug("Pool/set to standby "+obj);
            }
            return true;
        }
        else
        {
            // Happens when type wasn't defined in pooling yet.
            CallbackStandby(obj.GetComponentsInChildren<IPooling>());
            PoolingDebug("");
            PoolingDebug("WARNING:DESTROY FAILED: Group doesn't exist. Manually handle it._|_" + group+"/"+obj + "_|Manually add it(include prefab)");
            PoolingDebug("");
            return false;
        }
    }

    public GameObject CreateInstance(
        string registerUnderTag, 
        Transform prefab, 
        Vector2 pos, 
        Quaternion rotation)
    {
        PoolingDebug("Create instance under tag "+registerUnderTag);
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
            objs[i].OnPooledCreated();
        }
    }

    void CallbackStandby(IPooling[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnPooledDestroyed();
        }
    }
    private void CreateNewPoolType(int startSize, string group, Transform prefab, bool dynamic)
    {
        PoolingDebug("Create pool type with tag "+group);
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

    void PoolingDebug(string msg)
    {
        //Debug.Log(msg);
        debugMessages.Add(new DebugMessage(
            string.Format("{0:F2}| {1}", Time.time, msg)));
    }

    void PoolingDebug(string msg, UnityEngine.Object context)
    {
        //Debug.Log(msg, context);
        debugMessages.Add(new DebugMessage(msg, context));
    }

    public class PoolItem {
        internal GameObject instance;
        public bool isInPoolOnStandby = true;

        // object is disabled until recalled to next pool.
        internal void MarkAsStandby()
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

    public class PoolInstance {
        string group;
        Transform prefab;
        bool isSizeDynamic;
        int startSize;
        List<PoolItem> createdObjects;

        public string Group { get => group; }
        public Transform Prefab { get => prefab; }
        public string IsSizeDynamic { get => IsSizeDynamic; }
        public ReadOnlyCollection<PoolItem> CreatedObjects { get => createdObjects.AsReadOnly(); }

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
        public PoolItem AddExistingToPool(GameObject obj)
        {
            PoolItem item = new PoolItem() { instance = obj, isInPoolOnStandby = false };
            //item.SetToReady(); no position information.
            createdObjects.Add(item);
            return item;
        }

        /// <summary>
        /// True: succesfully marked as standby
        /// False: not found.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal bool FindAndMarkAsStandby(GameObject obj)
        {
            // if it exists, mark it as standby
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i] == null) continue;
                if (createdObjects[i].instance == obj)
                {
                    createdObjects[i].MarkAsStandby();
                    return true;
                }
            }
            return false;
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