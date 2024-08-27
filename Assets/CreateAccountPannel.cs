using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateAccountPannel : MonoBehaviour
{
    public TMP_InputField IF_Name;
    public TMP_InputField IF_Money;

    public TextMeshProUGUI ErrorMoneyMessage;

    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        if (manager == null)
        {
            Debug.LogError("Manager script not found in the scene!");
        }
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

            if (manager.Account.Contains(accountName))
            {
                // Afficher un message d'erreur si le compte existe déjà
                ErrorMoneyMessage.text = "Ce compte existe déjà !";
                Debug.LogError("Ce compte existe déjà !");
                return; // Sortir de la méthode si le compte existe déjà
            }
            // Essayer de convertir le texte du montant en entier
            if (float.TryParse(IF_Money.text, out initialMoney))
            {
                // Appeler la méthode addAccount du script Manager
                manager.addAccount(accountName, initialMoney);

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
    
}
