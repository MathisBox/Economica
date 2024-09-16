using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditCategoryPanel : MonoBehaviour
{
    public string CurrentName;
    public string CurrentDescription;
    public Sprite CurrentIcon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;



    public Image image;
    public TMP_InputField EditName;
    public TextMeshProUGUI NamePlaceHolder;
    public GameObject EditNameSaveButton;

    public TMP_InputField EditDescription;
    public TextMeshProUGUI DescriptionPlaceHolder;
    public GameObject EditDescriptionSaveButton;

    public GameObject ScrollviewIcon;
    public GameObject IconContainer;
    public GameObject IconSaveButton;


    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo()
    {
        image.sprite = CurrentIcon;
        Name.text = CurrentName;
        Description.text = CurrentDescription;
    }

    public void EditIcon() 
    {
        //ouvrir le panel d'icon depuis manager, le peupler pour en choisir un autre,
        //après le choix, on met l'ancien dans la list d'icon qui non pas de caté et on ajoute le nouveau qu'on retire de la list
    }

    public void ValidateIcon()
    {

    }
    public void Editname()
    {
        EditName.gameObject.SetActive(true);
        EditNameSaveButton.SetActive(true);
        NamePlaceHolder.text = CurrentName;




    }
    public void validateName()
    {
        CurrentName = EditName.text;
        EditName.gameObject.SetActive(false);
        EditNameSaveButton.SetActive(false);

        int index = manager.CategorySprite.IndexOf(CurrentIcon);
        manager.CategoryName[index] = CurrentName;

        UpdateInfo();

    }


    public void Editdescription()
    {
        EditDescription.gameObject.SetActive(true);
        EditDescriptionSaveButton.SetActive(true);
        DescriptionPlaceHolder.text = CurrentDescription;




    }
    public void ValidateDescritpion()
    {

        CurrentDescription = EditDescription.text;
        EditDescription.gameObject.SetActive(false);
        EditDescriptionSaveButton.SetActive(false);

        EditDescription.gameObject.SetActive(true);
        DescriptionPlaceHolder.text = CurrentDescription;

        int index = manager.CategorySprite.IndexOf(CurrentIcon);
        manager.CategoryDescription[index] = CurrentDescription;

        UpdateInfo();
    }


}
