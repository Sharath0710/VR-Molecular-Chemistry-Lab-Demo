using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoleculeUITester : MonoBehaviour
{
    public MoleculeData moleculeData;

    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI formulaText;
    public TextMeshProUGUI bondText;
    public Image bondImage;

    private MoleculeData lastData;

    void Update()
    {
        // Live update when changed in Inspector
        if (moleculeData != lastData)
        {
            UpdateUI();
            lastData = moleculeData;
        }
    }

    public void UpdateUI()
    {
        if (moleculeData == null) return;

        nameText.text = moleculeData.moleculeName;
        infoText.text = moleculeData.moleduleInfo;
        formulaText.text = moleculeData.formula;
        bondText.text = moleculeData.bondInfo;

        if (bondImage != null && moleculeData.bondImage != null)
        {
            bondImage.sprite = moleculeData.bondImage;
            bondImage.gameObject.SetActive(true);
        }
        else if (bondImage != null)
        {
            bondImage.gameObject.SetActive(false);
        }
    }
}