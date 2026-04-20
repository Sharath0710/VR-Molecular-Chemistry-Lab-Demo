using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LibraryItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image moleculeImage;

    public void Setup(MoleculeData data)
    {
        nameText.text = data.moleculeName + " (" + data.formula + ")";
        moleculeImage.sprite = data.bondImage;
    }
}