using UnityEngine;

[CreateAssetMenu]
public class SpawnItem : ScriptableObject {

    [SerializeField] ObjectReference prefab;
    [SerializeField] BoolReference canSpawn;
    [SerializeField] ObjectReferenceArray spawnedStorage;
    [SerializeField] ObjectReference spawnPointRef;
    [SerializeField] ObjectReference lastSpawned;

    public void Invoke()
    {
        if (canSpawn.Value)
        {
            lastSpawned.Value = Instantiate(prefab.Value, spawnPointRef.Value.transform.position, spawnPointRef.Value.transform.rotation);

            if (spawnedStorage)
            {
                spawnedStorage.Register(lastSpawned.Value.gameObject);
            }
        }
    }
}
