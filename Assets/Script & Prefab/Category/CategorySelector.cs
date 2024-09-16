using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelector : MonoBehaviour
{
    public GameObject iconPanel;        // Le panel qui contient les boutons d'ic�nes
    public Image buttonIcon;            // L'image actuelle du bouton principal
    public GameObject iconButtonPrefab; // Le prefab pour chaque bouton d'ic�ne
    public Transform iconContainer;     // Le conteneur o� les boutons seront ajout�s
    public List<string> iconNames;      // Liste des noms d'ic�nes (cha�nes de caract�res)
    public AddEntryExit addEntryExit;

    public Manager manager;

    void Start()
    {
        manager = FindObjectOfType<Manager>();

        // Assurez-vous que le panel est cach� au d�marrage
        iconPanel.SetActive(false);

        // Charger les ic�nes et remplir le panel au d�marrage
        PopulateIconPanel();
    }

    void PopulateIconPanel()
    {
        foreach (Sprite icon in manager.CategorySprite)
        {
            // Charger l'image depuis le dossier Resources

            if (icon != null)
            {
                Debug.Log(icon);
                // Cr�er un bouton d'ic�ne
                GameObject newButton = Instantiate(iconButtonPrefab, iconContainer);

                // D�finir l'image de l'ic�ne sur le bouton
                newButton.GetComponent<Image>().sprite = icon;

                // Ajouter l'action de clic pour attribuer l'ic�ne et fermer le panel
                newButton.GetComponent<Button>().onClick.AddListener(() => SetIcon(icon));
            }
            else
            {
                Debug.LogWarning($"Icone {icon}.png non trouv�e dans Resources.");
            }
        }
    }

    public void OpenIconPanel()
    {
        // Affiche le panel contenant les boutons d'ic�nes
        iconPanel.SetActive(true);
    }

    public void SetIcon(Sprite newIcon)
    {
        // Change l'ic�ne du bouton principal
        buttonIcon.sprite = newIcon;
        addEntryExit.category = newIcon.name;

        // Ferme le panel
        iconPanel.SetActive(false);

    }
}
