using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<string> Account;     // Liste des noms de compte
    public List<float> InitialMoneyAccount;   // Liste des montants d'argent associ�s aux comptes
    public List<float> MoneyAccount;   // Liste des montants d'argent associ�s aux comptes
    public List<GameObject> AccountInBar;
    public List<GameObject> entryExitObjects = new List<GameObject>();


    public Transform accountBarParent; 
    public GameObject accountPrefab;
    public AccountFilterManager filterManager;
    public string filter;

    public RectTransform rectTransform;

    public TextMeshProUGUI TotalMoneyText;
    public TextMeshProUGUI BelowTotalMoneyText;

    public Transform AddAccountPanelPlace;
    public GameObject AddAccountPanel;
    public GameObject AddEntryExitPanel;
    public GameObject FilterPanel;
    public Transform FilterPanelPlace;
    public Button AddEntryExitButton;
    public ScrollViewManager scrollViewManager;


    public bool AtoZ;
    

    // Start is called before the first frame update
    void Start()
    {
        



        if(Account == null || Account.Count == 0)
        {
            AddEntryExitButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void UpdateTotalMoney(string account)
    {
        int index = Account.IndexOf(account);

        if (account == "all")
        {
            float totalMoney = 0f;

            foreach (float money in MoneyAccount)
            {
                totalMoney += money;
            }

            TotalMoneyText.text = totalMoney.ToString();
            BelowTotalMoneyText.text = "Argent au toal";
            filter = null;
        }
        else
        {

            TotalMoneyText.text= MoneyAccount[index].ToString();
            BelowTotalMoneyText.text = "Total d'argent sur : " + account;
            filter = account;
        }
        ListEntryExitUpdate();
        // Ajouter tous les montants de la liste MoneyAccount � totalMoney

    }
    public void CreateAddAccountPanel()
    {
        GameObject Panel = Instantiate(AddAccountPanel, AddAccountPanelPlace);
    }
    public void CreateAddEntryExitPanel()
    {
        GameObject Panel = Instantiate(AddEntryExitPanel, AddAccountPanelPlace);
        Panel.GetComponent<AddEntryExit>().AccountList = Account;
        
    }

    public void CreateFilterPanel()
    {
        GameObject Panel = Instantiate(AddAccountPanel, AddAccountPanelPlace);
    }

    public void addAccount(string name, float initialMoney)
    {
        AddEntryExitButton.interactable = true;
        // Ajout du nom du compte dans la liste Account
        Account.Add(name);

        // Ajout du montant initial dans la liste MoneyAccount (au m�me index que le nom)
        InitialMoneyAccount.Add(initialMoney);
        MoneyAccount.Add(initialMoney);
        UpdateFilter();


        Debug.Log("Compte ajout�: " + name + " avec " + initialMoney + " euros.");
    }
    
    public void deleteAccount(string name)
    {
        // Trouver l'index du compte � supprimer
        int index = Account.IndexOf(name);

        if (index != -1)
        {
            // Supprimer le nom du compte de la liste Account
            Account.RemoveAt(index);

            // Supprimer le montant correspondant dans la liste MoneyAccount
            MoneyAccount.RemoveAt(index);
            UpdateFilter();


            Debug.Log("Compte supprim�: " + name);
        }
        else
        {
            Debug.Log("Compte non trouv�: " + name);
        }
    }

    public void UpdateFilter()
    {
        filterManager.UpdateFilterList(Account);
    }

    public void AddEntryExit(string name, float Money, string account)
    {
        scrollViewManager.AddEntry(name, Money, account);
        ListEntryExitUpdate();
        int index = Account.IndexOf(account);
        MoneyAccount[index] = MoneyAccount[index] + Money;

        if (filter == null)
        {
            UpdateTotalMoney("all");
        }
        else
        {
            UpdateTotalMoney(filter);

        }


    }

    public void DeleteEntryExit()
    {

    }

    public void moveScreenButton (RectTransform target)
    {
        StartCoroutine(MoveScreen(target));
    }
    public IEnumerator MoveScreen(RectTransform target)
    {
        float lerpDuration = 0.5f;  // Dur�e totale de l'interpolation
        float elapsedTime = 0f;     // Temps �coul� depuis le d�but de l'interpolation

        Vector3 startPosition = rectTransform.position;  // Position de d�part

        while (elapsedTime < lerpDuration)
        {
            // Augmenter le temps �coul�
            elapsedTime += Time.deltaTime;

            // Calculer le facteur d'interpolation
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            // Interpoler la position
            rectTransform.position = Vector3.Lerp(startPosition, target.position, t);

            // Attendre la prochaine frame
            yield return null;
        }

        // S'assurer que la position finale est exactement celle du target
        rectTransform.position = target.position;
    }




    public void ListEntryExitUpdate()
    {
        // Vider la liste actuelle pour �viter les doublons
        entryExitObjects.Clear();
        scrollViewManager.SetActiveEntryExit();

        // Rechercher tous les GameObjects avec le composant "EntryExit"
        EntryExit[] allEntriesExits = FindObjectsOfType<EntryExit>();

        // Parcourir chaque "EntryExit" trouv� et ajouter le GameObject � la liste
        foreach (EntryExit entryExit in allEntriesExits)
        {
            // V�rifier si le filtre est vide ou correspond au nom du compte
            if (string.IsNullOrEmpty(filter) || entryExit.Account == filter)
            {
                // Ajouter le GameObject � la liste filtr�e
                entryExitObjects.Add(entryExit.gameObject);
                entryExit.gameObject.SetActive(true);

                // Debug : Afficher le nom du compte et la valeur si cela correspond au filtre
                Debug.Log("Compte: " + entryExit.Account + ", Valeur: " + entryExit.Money);
            }
            else
            {
                entryExit.gameObject.SetActive(false);
            }
        }

        // Trier entryExitObjects en ordre alphab�tique par le nom du compte si AtoZ est vrai
        if (AtoZ)
        {
            // Trier la liste entryExitObjects par nom de compte
            entryExitObjects.Sort((a, b) =>
            {
                EntryExit entryA = a.GetComponent<EntryExit>();
                EntryExit entryB = b.GetComponent<EntryExit>();

                if (entryA == null || entryB == null)
                    return 0;

                return string.Compare(entryA.Title, entryB.Title, System.StringComparison.Ordinal);
            });

            // R�organiser les enfants dans la hi�rarchie Unity en fonction de l'ordre tri�
            for (int i = 0; i < entryExitObjects.Count; i++)
            {
                // D�finir l'index des enfants en fonction de l'ordre dans entryExitObjects
                entryExitObjects[i].transform.SetSiblingIndex(i);
            }
        }
    }


    }
