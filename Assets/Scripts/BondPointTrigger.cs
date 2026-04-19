using UnityEngine;

public class BondPointTrigger : MonoBehaviour
{
    private AtomController parentAtom;

    private void Awake()
    {
        parentAtom = GetComponentInParent<AtomController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AtomController otherAtom = other.GetComponentInParent<AtomController>();

        if (otherAtom == null) return;
        if (otherAtom == parentAtom) return;

        // CHECK
        if (!parentAtom.canBond || !otherAtom.canBond) return;

        BondManager.Instance.TryBond(parentAtom, otherAtom);
    }
}