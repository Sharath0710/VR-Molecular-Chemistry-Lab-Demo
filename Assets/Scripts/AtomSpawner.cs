using UnityEngine;

public class AtomSpawner : MonoBehaviour
{
    public static AtomSpawner Instance;

    public GameObject atomPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAtom(AtomData data, Vector3 position)
    {
        GameObject atom = Instantiate(atomPrefab, position, Quaternion.identity);

        var controller = atom.GetComponent<AtomController>();
        controller.atomData = data;

       Renderer rend = atom.GetComponentInChildren<Renderer>();
        if(rend != null)
        {
            rend.material.color = data.atomColor;
        }
    }
}