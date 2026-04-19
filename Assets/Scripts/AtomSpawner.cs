using UnityEngine;

public class AtomSpawner : MonoBehaviour
{
    public static AtomSpawner Instance;

    public GameObject hydrogenPrefab;
    public GameObject oxygenPrefab;
    public GameObject carbonPrefab;
    public GameObject nitrogenPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAtom(AtomData data, Vector3 position)
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

        // Safety check (VERY IMPORTANT)
        if (prefabToSpawn == null)
        {
            Debug.LogError("No prefab assigned for " + data.atomType);
            return;
        }

        GameObject atom = Instantiate(prefabToSpawn, position, Quaternion.identity);

        var controller = atom.GetComponent<AtomController>();
        controller.atomData = data;

        Renderer rend = atom.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = data.atomColor;
        }
    }
}