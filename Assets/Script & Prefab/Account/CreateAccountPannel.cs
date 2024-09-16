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
            // R�cup�rer les valeurs des InputFields
            string accountName = IF_Name.text;
            float initialMoney;

            if (manager.Account.Contains(accountName))
            {
                // Afficher un message d'erreur si le compte existe d�j�
                ErrorMoneyMessage.text = "Ce compte existe d�j� !";
                Debug.LogError("Ce compte existe d�j� !");
                return; // Sortir de la m�thode si le compte existe d�j�
            }
            // Essayer de convertir le texte du montant en entier
            if (float.TryParse(IF_Money.text, out initialMoney))
            {
                // Appeler la m�thode addAccount du script Manager
                manager.addAccount(accountName, initialMoney);

                // Fermer ou r�initialiser le panneau apr�s la cr�ation
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
