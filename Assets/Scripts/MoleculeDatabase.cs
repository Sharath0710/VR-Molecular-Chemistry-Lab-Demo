using System.Collections.Generic;
using UnityEngine;

public class MoleculeDatabase : MonoBehaviour
{
    public List<MoleculeData> molecules;

    public MoleculeData FindMatch(List<AtomType> input)
    {
        foreach (var molecule in molecules)
        {
            if (IsMatch(molecule.requiredAtoms, input))
                return molecule;
        }
        return null;
    }

    bool IsMatch(List<AtomType> a, List<AtomType> b)
    {
        if (a.Count != b.Count) return false;

        List<AtomType> temp = new List<AtomType>(b);

        foreach (var atom in a)
        {
            if (!temp.Remove(atom))
                return false;
        }

        return true;
    }
}