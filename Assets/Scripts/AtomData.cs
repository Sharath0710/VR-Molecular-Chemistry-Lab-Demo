using UnityEngine;

[CreateAssetMenu(menuName = "XR/Atom Data")]
public class AtomData : ScriptableObject
{
    public AtomType atomType;
    public int maxBonds;
    public Color atomColor;
}