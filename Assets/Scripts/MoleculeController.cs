using System.Collections.Generic;
using UnityEngine;

public class MoleculeController : MonoBehaviour
{
    private List<AtomController> atoms;

    public void Initialize(List<AtomController> atomList)
    {
        if (atomList == null)
        {
            Debug.LogError("Initialize called with NULL atoms!");
            return;
        }

        atoms = new List<AtomController>(atomList); // 🔥 COPY LIST (IMPORTANT)
    }

    public void ResetToAtoms()
    {
        if (atoms == null)
        {
            Debug.LogError("Atoms list is NULL in Reset!");
            Destroy(gameObject);
            return;
        }

        foreach (var atom in atoms)
        {
            if (atom != null)
            {
                atom.transform.SetParent(null);
                atom.gameObject.SetActive(true);

                atom.connectedAtoms.Clear();

                Rigidbody rb = atom.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        Destroy(gameObject);
    }
}