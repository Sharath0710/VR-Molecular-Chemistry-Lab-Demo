using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    public static BondManager Instance;

    [Header("References")]
    public GameObject bondPrefab;
    public MoleculeDatabase database;

    private void Awake()
    {
        Instance = this;
    }

    public void TryBond(AtomController a, AtomController b)
    {
        // Safety checks
        if (a == null || b == null || a == b)
            return;

        if (!a.HasFreeBond() || !b.HasFreeBond())
            return;

        // Prevent duplicate bonding
        if (a.connectedAtoms.Contains(b))
            return;

        var pointA = a.GetFreeBondPoint();
        var pointB = b.GetFreeBondPoint();

        if (pointA == null || pointB == null)
            return;

        pointA.Occupy();
        pointB.Occupy();

        CreateBond(pointA.transform.position, pointB.transform.position);

        ConnectAtoms(a, b);

        // Check for molecule formation
        var group = GetConnectedGroup(a);

        List<AtomType> types = new List<AtomType>();

        foreach (var atom in group)
        {
            if (atom != null && atom.atomData != null)
                types.Add(atom.atomData.atomType);
        }

        var match = database != null ? database.FindMatch(types) : null;

        if (match != null)
        {
            StartCoroutine(CreateMoleculeDelayed(match, group));
        }
    }

    void CreateBond(Vector3 a, Vector3 b)
    {
        if (bondPrefab == null)
            return;

        Vector3 mid = (a + b) / 2;
        Vector3 dir = b - a;

        GameObject bond = Instantiate(bondPrefab, mid, Quaternion.identity);

        bond.transform.up = dir.normalized;
        bond.transform.localScale = new Vector3(0.02f, dir.magnitude / 2, 0.02f);
    }

    void ConnectAtoms(AtomController a, AtomController b)
    {
        if (!a.connectedAtoms.Contains(b))
            a.connectedAtoms.Add(b);

        if (!b.connectedAtoms.Contains(a))
            b.connectedAtoms.Add(a);
    }

    List<AtomController> GetConnectedGroup(AtomController start)
    {
        List<AtomController> group = new List<AtomController>();
        Queue<AtomController> queue = new Queue<AtomController>();

        queue.Enqueue(start);
        group.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in current.connectedAtoms)
            {
                if (neighbor != null && !group.Contains(neighbor))
                {
                    group.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return group;
    }

    IEnumerator CreateMoleculeDelayed(MoleculeData data, List<AtomController> atoms)
    {
        yield return null; // wait 1 frame (important for stability)

        Vector3 center = Vector3.zero;
        int count = 0;

        foreach (var atom in atoms)
        {
            if (atom != null)
            {
                center += atom.transform.position;
                count++;
            }
        }

        if (count == 0)
            yield break;

        center /= count;

        // Destroy atoms safely
        foreach (var atom in atoms)
        {
            if (atom != null)
                Destroy(atom.gameObject);
        }

        // Spawn molecule prefab
        if (data.moleculePrefab != null)
        {
            Instantiate(data.moleculePrefab, center, Quaternion.identity);
        }
    }
}