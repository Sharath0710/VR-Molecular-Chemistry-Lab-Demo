using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public MoleculeController currentMolecule;

    [Header("Panels")]
    public GameObject welcomePanel;
    public GameObject moleculePanel;

    [Header("Molecule UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI formulaText;
    public TextMeshProUGUI bondText;
    public TextMeshProUGUI infoText;
    public Image moleculeImage;

    public GameObject libraryPanel;


    [Header("Library UI")]
    public Transform libraryContainer;
    public GameObject libraryItemPrefab;

    private HashSet<string> discovered = new HashSet<string>();

    private void Awake()
    {
        Instance = this;

        welcomePanel.SetActive(true);
        moleculePanel.SetActive(false);
    }

    // 🔹 Called by Start Button
    public void StartExperience()
    {
        AnimateOut(welcomePanel);
    }

    

    // 🔹 Show Molecule Info
    public void ShowMolecule(MoleculeData data, MoleculeController molecule)
    {
        nameText.text = data.moleculeName;
        formulaText.text = data.formula;
        bondText.text = data.bondInfo;
        infoText.text = data.moleduleInfo;
        moleculeImage.sprite = data.bondImage;

        moleculePanel.SetActive(true);
        AnimateIn(moleculePanel);
    }

    public void HideMolecule()
    {
        AnimateOut(moleculePanel);
    }

    // 🔥 ANIMATION HELPERS (LeanTween)

    void AnimateIn(GameObject panel)
    {
        panel.SetActive(true);

        panel.transform.localScale = Vector3.zero;

        LeanTween.scale(panel, new Vector3(0.001f, 0.001f, 0.001f), 0.35f)
            .setEaseOutBack();
    }

    void AnimateOut(GameObject panel)
    {
        LeanTween.scale(panel, Vector3.zero, 0.25f)
            .setEaseInBack()
            .setOnComplete(() => panel.SetActive(false));
    }

    public void AddToLibrary(MoleculeData data)
    {
        if (discovered.Contains(data.moleculeName))
            return;

        discovered.Add(data.moleculeName);

        GameObject item = Instantiate(libraryItemPrefab, libraryContainer);

        var text = item.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = data.moleculeName + " (" + data.formula + ")";
    }

    public void OnResetClicked()
    {
        if (currentMolecule == null)
        {
            Debug.LogWarning("No molecule to reset");
            return;
        }

        currentMolecule.ResetToAtoms();
        currentMolecule = null;

        AnimateOut(moleculePanel);
    }



    public void ToggleLibrary()
    {
        libraryPanel.SetActive(!libraryPanel.activeSelf);
    }
}