using UnityEngine;

public class AtomSpawnPoint : MonoBehaviour
{
    public AtomData atomData;

    private void Start()
    {
        AtomSpawner.Instance.SpawnAtom(atomData, transform);
    }
}