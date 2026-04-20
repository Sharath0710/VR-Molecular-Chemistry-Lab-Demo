using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomGroup : MonoBehaviour
{
    private float idleTimer = 0f;
    private float maxIdleTime = 10f;

    private bool isActive = false;

    public void MarkActive()
    {
        idleTimer = 0f;
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;

        idleTimer += Time.deltaTime;

        if (idleTimer >= maxIdleTime)
        {
            BreakGroup();
        }
    }

    void BreakGroup()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        foreach (var child in children)
        {
            child.SetParent(null);

            var atom = child.GetComponent<AtomController>();
            if (atom != null)
            {
                atom.connectedAtoms.Clear();
                atom.ResetBonds();
            }
        }

        Destroy(gameObject);
    }
}