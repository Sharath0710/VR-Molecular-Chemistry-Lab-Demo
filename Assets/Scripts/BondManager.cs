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

        //  Only allow bonding if ONE of them is held
        if (!a.isHeld && !b.isHeld)
            return;

        //  Prevent same group bonding again
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
    Transform rootA = a.transform.root;
    Transform rootB = b.transform.root;

    if (rootA == rootB)
        return;

    GameObject group = new GameObject("AtomGroup");

    // 🔥 ADD + STORE reference
    AtomGroup groupScript = group.AddComponent<AtomGroup>();

    MoveToGroup(rootA, group.transform);
    MoveToGroup(rootB, group.transform);

    // 🔥 THIS WAS MISSING
    groupScript.MarkActive();
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



        GameObject mol = Instantiate(data.moleculePrefab, center, Quaternion.identity);

        List<AtomController> atomCopy = new List<AtomController>(atoms);

        var controller = mol.AddComponent<MoleculeController>();
        controller.Initialize(atomCopy);
        Debug.Log("Controller created: " + controller.GetInstanceID());

        Transform groupRoot = atoms[0].transform.root;

        if (groupRoot.name == "AtomGroup")
        {
            foreach (Transform child in groupRoot)
            {
                child.SetParent(null);
            }

            Destroy(groupRoot.gameObject);
        }
        foreach (var atom in atomCopy)
        {
            atom.gameObject.SetActive(false);
        }

        Debug.Log("Atoms count passed to molecule: " + atomCopy.Count);

        UIManager.Instance.ShowMolecule(data, controller);
        UIManager.Instance.AddToLibrary(data);


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBondSound();
        }
    }

    void MoveToGroup(Transform root, Transform group)
    {
        if (root.name == "AtomGroup")
        {
            AtomGroup existingGroup = root.GetComponent<AtomGroup>();

            List<Transform> children = new List<Transform>();

            foreach (Transform child in root)
                children.Add(child);

            foreach (var child in children)
                child.SetParent(group);

            // 🔥 RESET TIMER when merging groups
            if (existingGroup != null)
            {
                AtomGroup newGroup = group.GetComponent<AtomGroup>();
                if (newGroup != null)
                    newGroup.MarkActive();
            }

            Destroy(root.gameObject);
        }
        else
        {
            root.SetParent(group);
        }
    }
}