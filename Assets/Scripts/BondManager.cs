using UnityEngine;

public class BondManager : MonoBehaviour
{
    public static BondManager Instance;

    public GameObject bondPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void TryBond(AtomController a, AtomController b)
    {
        if (!a.HasFreeBond() || !b.HasFreeBond())
            return;

        var pointA = a.GetFreeBondPoint();
        var pointB = b.GetFreeBondPoint();

        if (pointA == null || pointB == null)
            return;

        pointA.Occupy();
        pointB.Occupy();

        CreateBond(pointA.transform.position, pointB.transform.position);
    }

    void CreateBond(Vector3 a, Vector3 b)
    {
        Vector3 mid = (a + b) / 2;
        Vector3 dir = b - a;

        GameObject bond = Instantiate(bondPrefab, mid, Quaternion.identity);

        bond.transform.up = dir.normalized;
        bond.transform.localScale = new Vector3(0.02f, dir.magnitude / 2, 0.02f);
    }
}