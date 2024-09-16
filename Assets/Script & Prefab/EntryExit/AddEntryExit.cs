using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddEntryExit : MonoBehaviour
{
    public TMP_InputField IF_Name;
    public TMP_InputField IF_Money;

    public TextMeshProUGUI ErrorMoneyMessage;

    public TMP_Dropdown dropdown;  
    public List<string> AccountList;
    public string valueSelected;
    
    
    public Manager manager;

    public DateTime Date;
    public TextMeshProUGUI DateText;

    public string category;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        if (manager == null)
        {
            Debug.LogError("Manager script not found in the scene!");
        }
        CreateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        UpdateDate(DateTime.Now);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidateCreation()
    {
        if (manager != null)
        {
            // Récupérer les valeurs des InputFields
            string accountName = IF_Name.text;
            float initialMoney;

           
            // Essayer de convertir le texte du montant en entier
            if (float.TryParse(IF_Money.text, out initialMoney))
            {
                if (valueSelected == "")
                {
                    valueSelected = AccountList[0];
                }
                // Appeler la méthode addAccount du script Manager
                manager.AddEntryExit(accountName, initialMoney, valueSelected, Date, category);

                // Fermer ou réinitialiser le panneau après la création
                CancelCreation();
            }
            else
            {
                ErrorMoneyMessage.text = "Le montant saisi n'est pas valide !";
                Debug.LogError("Le montant saisi n'est pas valide !");
            }
        }
    }
    public void CancelCreation()
    {
        Destroy(this.gameObject);
    }

    void CreateDropdown()
    {
        dropdown.ClearOptions(); // Efface les options existantes
        dropdown.AddOptions(AccountList); // Ajoute les nouvelles options
    }

    // Méthode pour gérer le changement de sélection du Dropdown
    public void OnDropdownValueChanged(int index)
    {

        Debug.Log("Option sélectionnée : " + AccountList[index]);
        valueSelected = AccountList[index];
        
    }



    //Callendar
    public void UpdateDate(DateTime date)
    {
        Date = date;
        DateText.text = date.ToShortDateString();
    }
    

}
