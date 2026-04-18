using UnityEngine;

public class BondPoint : MonoBehaviour
{
    public bool isOccupied = false;

    private MeshRenderer visual;

    private void Awake()
    {
        visual = GetComponent<MeshRenderer>();
        SetVisible(false);
    }

    public void SetVisible(bool state)
    {
        if (visual != null)
            visual.enabled = state;
    }

    public void Occupy()
    {
        isOccupied = true;
        SetVisible(false);
    }
}