using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "XR/Molecule")]
public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public string moleduleInfo;
    public string formula;
    public string bondInfo;
    public Sprite bondImage;

    public List<AtomType> requiredAtoms;

    public GameObject moleculePrefab;
}