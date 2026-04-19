using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomController : XRGrabInteractable
{
    public AtomData atomData;

    private List<BondPoint> bondPoints = new List<BondPoint>();
    public List<AtomController> connectedAtoms = new List<AtomController>();

    protected override void Awake()
    {
        base.Awake();
        bondPoints.AddRange(GetComponentsInChildren<BondPoint>());
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        AtomSpawner.Instance.SpawnAtom(atomData, transform.position);

        ShowBondPoints(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ShowBondPoints(false);
    }

    public bool HasFreeBond()
    {
        foreach (var point in bondPoints)
        {
            if (!point.isOccupied)
                return true;
        }
        return false;
    }

    public BondPoint GetFreeBondPoint()
    {
        foreach (var point in bondPoints)
        {
            if (!point.isOccupied)
                return point;
        }
        return null;
    }

    void ShowBondPoints(bool state)
    {
        foreach (var point in bondPoints)
        {
            point.SetVisible(state);
        }
    }
}