using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryPanel : MonoBehaviour
{

    public Manager manager;
    public GameObject iconButtonPrefab;
    public Transform iconContainer;

    public GameObject EditCategoryPanel;
    public GameObject ActualEditCategoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        SetupPanell();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetupPanell()
    {
        for(int i = 0;i < iconContainer.childCount;i++)
        {

            Destroy(iconContainer.GetChild(i).gameObject);

        }

        foreach (Sprite icon in manager.CategorySprite)
        {
            if (manager.CategorySprite != null)
            {
                // Créer un bouton d'icône
                GameObject newButton = Instantiate(iconButtonPrefab, iconContainer);

                // Définir l'image de l'icône sur le bouton
                newButton.GetComponent<Image>().sprite = icon;

                // Ajouter l'action de clic pour attribuer l'icône et fermer le panel
                newButton.GetComponent<Button>().onClick.AddListener(() => SelectIcon(icon));
            }
            else
            {
                Debug.LogWarning($"Icone {icon}.png non trouvée dans Resources.");
            }

        }

        GameObject addButton = Instantiate(iconButtonPrefab, iconContainer);
        addButton.GetComponent<Button>().onClick.AddListener(() => manager.CreateAddCategoryPanel(this));

    }

    public void SelectIcon(Sprite icon)
    {
        Destroy(ActualEditCategoryPanel);
        GameObject newPanel = Instantiate(EditCategoryPanel, manager.gameObject.transform);
        
        int index = manager.CategorySprite.IndexOf(icon);

        ActualEditCategoryPanel = newPanel;


        newPanel.GetComponent<EditCategoryPanel>().CurrentName = manager.CategoryName[index];
        newPanel.GetComponent<EditCategoryPanel>().CurrentIcon = manager.CategorySprite[index];
        newPanel.GetComponent<EditCategoryPanel>().CurrentDescription = manager.CategoryDescription[index];

    }
}
