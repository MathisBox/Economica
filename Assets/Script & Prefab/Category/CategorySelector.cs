using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelector : MonoBehaviour
{
    public GameObject iconPanel;        // Le panel qui contient les boutons d'icônes
    public Image buttonIcon;            // L'image actuelle du bouton principal
    public GameObject iconButtonPrefab; // Le prefab pour chaque bouton d'icône
    public Transform iconContainer;     // Le conteneur où les boutons seront ajoutés
    public List<string> iconNames;      // Liste des noms d'icônes (chaînes de caractères)
    public AddEntryExit addEntryExit;

    public Manager manager;

    void Start()
    {
        manager = FindObjectOfType<Manager>();

        // Assurez-vous que le panel est caché au démarrage
        iconPanel.SetActive(false);

        // Charger les icônes et remplir le panel au démarrage
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
                // Créer un bouton d'icône
                GameObject newButton = Instantiate(iconButtonPrefab, iconContainer);

                // Définir l'image de l'icône sur le bouton
                newButton.GetComponent<Image>().sprite = icon;

                // Ajouter l'action de clic pour attribuer l'icône et fermer le panel
                newButton.GetComponent<Button>().onClick.AddListener(() => SetIcon(icon));
            }
            else
            {
                Debug.LogWarning($"Icone {icon}.png non trouvée dans Resources.");
            }
        }
    }

    public void OpenIconPanel()
    {
        // Affiche le panel contenant les boutons d'icônes
        iconPanel.SetActive(true);
    }

    public void SetIcon(Sprite newIcon)
    {
        // Change l'icône du bouton principal
        buttonIcon.sprite = newIcon;
        addEntryExit.category = newIcon.name;

        // Ferme le panel
        iconPanel.SetActive(false);

    }
}
