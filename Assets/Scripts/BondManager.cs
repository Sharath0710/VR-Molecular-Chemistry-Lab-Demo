using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    public static BondManager Instance;

    public MoleculeDatabase database;

    private void Awake()
    {
        Instance = this;
    }

    public void TryBond(AtomController a, AtomController b)
    {
        if (a == null || b == null || a == b)
            return;

        if (!a.canBond || !b.canBond)
            return;

        if (a.connectedAtoms.Contains(b))
            return;

        // 🔥 Prevent same group bonding again
        if (a.transform.root == b.transform.root)
            return;

        ConnectAtoms(a, b);
        GroupAtoms(a, b);

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
            CreateMolecule(match, group);
        }
    }

    void ConnectAtoms(AtomController a, AtomController b)
    {
        if (!a.connectedAtoms.Contains(b))
            a.connectedAtoms.Add(b);

        if (!b.connectedAtoms.Contains(a))
            b.connectedAtoms.Add(a);
    }

    void GroupAtoms(AtomController a, AtomController b)
    {
        Transform root = a.transform.root;

        b.transform.SetParent(root);

        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
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

    void CreateMolecule(MoleculeData data, List<AtomController> atoms)
    {
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

        if (count == 0) return;

        center /= count;

        foreach (var atom in atoms)
        {
            atom.gameObject.SetActive(false);
        }

        GameObject mol = Instantiate(data.moleculePrefab, center, Quaternion.identity);

        var controller = mol.AddComponent<MoleculeController>();
        controller.Initialize(atoms);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowMolecule(data, controller);
            UIManager.Instance.AddToLibrary(data);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBondSound();
        }
    }
}