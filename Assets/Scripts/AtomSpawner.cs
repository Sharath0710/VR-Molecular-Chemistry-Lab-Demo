using UnityEngine;
using System.Collections;

public class AtomSpawner : MonoBehaviour
{
    public static AtomSpawner Instance;

    public GameObject hydrogenPrefab;
    public GameObject oxygenPrefab;
    public GameObject carbonPrefab;
    public GameObject nitrogenPrefab;
    public float defaultSpawnDelay = 1f;
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAtom(AtomData data, Transform spawnPoint)
    {
        GameObject prefabToSpawn = null;

        switch (data.atomType)
        {
            case AtomType.Hydrogen:
                prefabToSpawn = hydrogenPrefab;
                break;
            case AtomType.Oxygen:
                prefabToSpawn = oxygenPrefab;
                break;
            case AtomType.Carbon:
                prefabToSpawn = carbonPrefab;
                break;
            case AtomType.Nitrogen:
                prefabToSpawn = nitrogenPrefab;
                break;
        }

        // 🔴 SAFETY CHECK
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab not assigned for: " + data.atomType);
            return;
        }

        // 🔥 Slight offset to avoid instant overlap
        Vector3 spawnPos = spawnPoint.position + Random.insideUnitSphere * 0.05f;

        GameObject atom = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        var controller = atom.GetComponent<AtomController>();
        controller.atomData = data;
        controller.SetSpawnPoint(spawnPoint);

        Renderer rend = atom.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = data.atomColor;
        }
        

        // 🔥 Delay bonding ONLY for newly spawned atoms
        StartCoroutine(EnableBondingDelayed(controller));
    }

    private IEnumerator EnableBondingDelayed(AtomController atom)
    {
        if (atom == null) yield break;

        atom.canBond = false;

        yield return new WaitForSeconds(defaultSpawnDelay);

        atom.EnableBonding();
    }
}