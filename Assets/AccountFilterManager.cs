using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // N'oubliez pas d'importer TextMeshPro pour le support du texte

public class AccountFilterManager : MonoBehaviour
{
    public GameObject buttonPrefab;              // Prefab du bouton � utiliser
    public Transform buttonParent;               // Parent des boutons dans la hi�rarchie UI
    public List<string> filterNames;             // Liste des noms de filtres
    public List<Button> filterButtons = new List<Button>(); // Liste des boutons de filtre

    public Manager manager;

    public bool aphabeticalFilter;

    void Start()
    {
        // Cr�er le bouton "All" en premier
        CreateFilterButton("All", true);

        // Cr�er des boutons pour chaque nom dans filterNames
        foreach (string filterName in filterNames)
        {
            CreateFilterButton(filterName, false);
        }

        
    }

    // M�thode pour cr�er un bouton de filtre
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

    // M�thode appel�e lorsque l'on clique sur un bouton de filtre
    void OnFilterButtonClick(string filterName)
    {
        if (filterName == "All")
        {
            // Si le filtre "All" est cliqu�, activer tous les filtres
            SetAllFilters();
        }
        else
        {
            // Activer uniquement le filtre s�lectionn�
            SetFilter(filterName);
        }
    }

    // M�thode pour activer tous les filtres ("All" s�lectionn�)
    void SetAllFilters()
    {
        foreach (Button btn in filterButtons)
        {
            SetButtonColor(btn, true);
        }
        manager.UpdateTotalMoney("all");


    }

    // M�thode pour activer un filtre sp�cifique
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


    // M�thode pour d�finir la couleur d'un bouton
    void SetButtonColor(Button button, bool isActive)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = isActive ? Color.white : Color.gray;
        colors.highlightedColor = isActive ? Color.white : Color.gray;
        button.colors = colors;
    }

    // M�thode publique pour mettre � jour la liste des filtres
    public void UpdateFilterList(List<string> newFilterNames)
    {
        // Mettre � jour la liste filterNames avec la nouvelle liste
        filterNames = new List<string>(newFilterNames);

        // Effacer tous les boutons existants
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
        filterButtons.Clear();

        // Recr�er le bouton "All"
        CreateFilterButton("All", true);

        // Recr�er les boutons pour la nouvelle liste de filtres
        foreach (string filterName in filterNames)
        {
            CreateFilterButton(filterName, false);
        }

        // D�finir le filtre "All" comme s�lectionn� par d�faut
        SetAllFilters();
    }
}
