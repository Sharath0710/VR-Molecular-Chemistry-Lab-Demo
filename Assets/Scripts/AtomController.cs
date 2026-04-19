using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomController : XRGrabInteractable
{
    public AtomData atomData;

    private List<BondPoint> bondPoints = new List<BondPoint>();
    public List<AtomController> connectedAtoms = new List<AtomController>();

    //track if this atom already spawned another
    private bool hasSpawned = false;

    // reference to spawn point
    private Transform spawnPoint;

    public bool canBond = false;


    private float idleTimer = 0f;
    private float maxIdleTime = 15f;

    private bool isGrabbed = false;
    private bool isFading = false;
    bool hasBeenUsed = false;


    protected override void Awake()
    {
        base.Awake();
        bondPoints.AddRange(GetComponentsInChildren<BondPoint>());
    }
    void Update()
    {
        // ❌ Never destroy if never used
        if (!hasBeenUsed)
            return;

        // ❌ Don't destroy if bonded
        if (connectedAtoms.Count > 0)
            return;

        // ❌ Don't destroy if grabbed
        if (isGrabbed)
            return;

        // ❌ Don't start multiple fades
        if (isFading)
            return;

        idleTimer += Time.deltaTime;

        if (idleTimer >= maxIdleTime)
        {
            StartCoroutine(DestroyWithFade());
        }
    }




    // Called from spawner
    public void SetSpawnPoint(Transform sp)
    {
        spawnPoint = sp;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //  Spawn ONLY first time this atom is grabbed
        if (!hasSpawned)
        {
            if (AtomSpawner.Instance != null && spawnPoint != null)
            {
                AtomSpawner.Instance.SpawnAtom(atomData, spawnPoint);
                hasSpawned = true;
            }
        }

        ShowBondPoints(true);
        isGrabbed = true;
        hasBeenUsed = true;
        idleTimer = 0f;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ShowBondPoints(false);
        isGrabbed = false;
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

    public void EnableBonding()
    {
        canBond = true;
    }

    IEnumerator DestroyWithFade()
    {
        isFading = true;

        float t = 0f;

        Renderer rend = GetComponentInChildren<Renderer>();

        if (rend == null)
        {
            Destroy(gameObject);
            yield break;
        }

        Material mat = rend.material;

        Color startColor = mat.color;

        while (t < 1f)
        {
            t += Time.deltaTime;

            Color c = startColor;
            c.a = 1 - t;

            mat.color = c;

            yield return null;
        }

        Destroy(gameObject);
    }
}