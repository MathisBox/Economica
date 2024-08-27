using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // N'oubliez pas d'importer TextMeshPro pour le support du texte

public class AccountFilterManager : MonoBehaviour
{
    public GameObject buttonPrefab;              // Prefab du bouton à utiliser
    public Transform buttonParent;               // Parent des boutons dans la hiérarchie UI
    public List<string> filterNames;             // Liste des noms de filtres
    public List<Button> filterButtons = new List<Button>(); // Liste des boutons de filtre

    public Manager manager;

    public bool aphabeticalFilter;

    void Start()
    {
        // Créer le bouton "All" en premier
        CreateFilterButton("All", true);

        // Créer des boutons pour chaque nom dans filterNames
        foreach (string filterName in filterNames)
        {
            CreateFilterButton(filterName, false);
        }

        
    }

    // Méthode pour créer un bouton de filtre
    void CreateFilterButton(string filterName, bool isAll)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonParent);
        newButton.name = filterName;

        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = filterName;
        }

        Button buttonComponent = newButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => OnFilterButtonClick(filterName));
            filterButtons.Add(buttonComponent);
        }
    }

    // Méthode appelée lorsque l'on clique sur un bouton de filtre
    void OnFilterButtonClick(string filterName)
    {
        if (filterName == "All")
        {
            // Si le filtre "All" est cliqué, activer tous les filtres
            SetAllFilters();
        }
        else
        {
            // Activer uniquement le filtre sélectionné
            SetFilter(filterName);
        }
    }

    // Méthode pour activer tous les filtres ("All" sélectionné)
    void SetAllFilters()
    {
        foreach (Button btn in filterButtons)
        {
            SetButtonColor(btn, true);
        }
        manager.UpdateTotalMoney("all");


    }

    // Méthode pour activer un filtre spécifique
    void SetFilter(string filterName)
    {
        foreach (Button btn in filterButtons)
        {
            SetButtonColor(btn, btn.name == filterName);
        }
        manager.UpdateTotalMoney(filterName);
    }


    void SetFilters(string type)
    {
        if(type == "AtoZ")
        {
            manager.entryExitObjects.Sort();
        }
    }


    // Méthode pour définir la couleur d'un bouton
    void SetButtonColor(Button button, bool isActive)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = isActive ? Color.white : Color.gray;
        colors.highlightedColor = isActive ? Color.white : Color.gray;
        button.colors = colors;
    }

    // Méthode publique pour mettre à jour la liste des filtres
    public void UpdateFilterList(List<string> newFilterNames)
    {
        // Mettre à jour la liste filterNames avec la nouvelle liste
        filterNames = new List<string>(newFilterNames);

        // Effacer tous les boutons existants
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
        filterButtons.Clear();

        // Recréer le bouton "All"
        CreateFilterButton("All", true);

        // Recréer les boutons pour la nouvelle liste de filtres
        foreach (string filterName in filterNames)
        {
            CreateFilterButton(filterName, false);
        }

        // Définir le filtre "All" comme sélectionné par défaut
        SetAllFilters();
    }
}
