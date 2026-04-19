using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XR/Molecule")]
public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public string formula;

    public List<AtomType> requiredAtoms;

    public GameObject moleculePrefab;
}