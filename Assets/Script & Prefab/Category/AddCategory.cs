using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddCategory : MonoBehaviour
{
    public Manager manager;


    public TMP_InputField Name;
    public Button buttonIcon;
    public TMP_InputField Description;

    public List<string> IconNameList;
    public GameObject iconButtonPrefab;
    public Transform iconContainer;
    public GameObject IconPannel;

    public string CategoryName;
    public Sprite CategorySprite;
    public string CategoryDescription;

    public CategoryPanel categoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        IconPannel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Validate()
    {
        if(CategorySprite != null)
        {


            CategoryName = Name.text;
            CategoryDescription = Description.text;

            manager.AddCategory(CategoryName, CategorySprite, CategoryDescription);

            categoryPanel.SetupPanell();
            Cancel();
        }
        else
        {

            //Error

        }
        
    }
    public void Cancel()
    {

        Destroy(gameObject);
    }
    public void CreateIconPannel()
    {
        for(int i = 0; i <  iconContainer.childCount; i++)
        {
            Destroy(iconContainer.GetChild(i).gameObject);
        }
        IconPannel.SetActive(true);
        foreach (string iconName in IconNameList)
        {
            // Charger l'image depuis le dossier Resources
            Sprite iconSprite = Resources.Load<Sprite>(iconName);

            if (iconSprite != null)
            {
                Debug.Log(iconName);
                // Créer un bouton d'icône
                GameObject newButton = Instantiate(iconButtonPrefab, iconContainer);

                // Définir l'image de l'icône sur le bouton
                newButton.GetComponent<Image>().sprite = iconSprite;

                // Ajouter l'action de clic pour attribuer l'icône et fermer le panel
                newButton.GetComponent<Button>().onClick.AddListener(() => SetIcon(iconSprite));
            }
            else
            {
                Debug.LogWarning($"Icone {iconName}.png non trouvée dans Resources.");
            }
        }
    }
    public void SetIcon(Sprite newIcon)
    {
        // Change l'icône du bouton principal
        buttonIcon.image.sprite = newIcon;
        CategorySprite = newIcon;
        IconPannel.SetActive(false);




    }
}
