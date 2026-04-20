using System.Collections.Generic;
using UnityEngine;

public class MoleculeController : MonoBehaviour
{
    private List<AtomController> atoms;

    public void Initialize(List<AtomController> atomList)
    {
        atoms = new List<AtomController>(atomList);

        Debug.Log("Initialized atoms count: " + atoms.Count);
    }

    public void ResetToAtoms()
    {
        if (atoms == null || atoms.Count == 0)
        {
            Debug.LogError("Atoms list is NULL or empty in Reset!");
            Destroy(gameObject);
            return;
        }

        foreach (var atom in atoms)
        {
            if (atom != null)
            {
                // 🔥 UNPARENT FIRST
                atom.transform.SetParent(null);

                // 🔥 RESET TRANSFORM (important)
                atom.transform.rotation = Quaternion.identity;

                // 🔥 ENABLE BACK
                atom.gameObject.SetActive(true);

                // 🔥 CLEAR CONNECTIONS
                atom.connectedAtoms.Clear();

                // 🔥 RESET PHYSICS
                Rigidbody rb = atom.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                // 🔥 RESET BOND STATE AND RESET STATE

                atom.ResetBonds();
                atom.ResetState(); 
            }
        }
        
        // 🔥 DESTROY MOLECULE
        Destroy(this.gameObject);
    }
}