using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using System;

public class Manager : MonoBehaviour
{
    public List<string> Account;     // Liste des noms de compte
    public List<float> InitialMoneyAccount;   // Liste des montants d'argent associ�s aux comptes
    public List<float> MoneyAccount;   // Liste des montants d'argent associ�s aux comptes
    public List<GameObject> AccountInBar;
    public List<GameObject> entryExitObjects = new List<GameObject>();

    public List<string> CategoryName;
    public List<Sprite> CategorySprite;
    public List<string> CategoryDescription;

    public GameObject AddCategoryPanel;
    public GameObject CategoryPanel;



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


    public bool DateCroissant;
    

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

    public void AddEntryExit(string name, float Money, string account, DateTime date, string category)
    {
        scrollViewManager.AddEntry(name, Money, account, date, category);
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
    public void DateCroissantFilter()
    {
        DateCroissant = true;
        ListEntryExitUpdate();

    }
    public void AddCategory(string name, Sprite sprite, string description)
    {
        CategoryName.Add(name);
        CategorySprite.Add(sprite);
        CategoryDescription.Add(description);
    }

    public void CreateAddCategoryPanel(CategoryPanel categoryPanel)
    {
        GameObject Panel = Instantiate(AddCategoryPanel, this.gameObject.transform);
        Panel.GetComponent<AddCategory>().categoryPanel = categoryPanel;
    }

    public void CreateCategoryPanel()
    {

        CategoryPanel existingPanel = FindAnyObjectByType<CategoryPanel>();

        if (existingPanel != null)
        {
            GameObject CategoryPanel = FindAnyObjectByType<CategoryPanel>().gameObject;
            Destroy(CategoryPanel);

        }
        else
        {

            GameObject Panel = Instantiate(CategoryPanel, this.gameObject.transform);

        }

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
        if (DateCroissant)
        {
            entryExitObjects.Sort((obj1, obj2) =>
            {
                EntryExit entryExit1 = obj1.GetComponent<EntryExit>();
                EntryExit entryExit2 = obj2.GetComponent<EntryExit>();

                // Comparer les dates de mani�re d�croissante
                return entryExit2.Date.CompareTo(entryExit1.Date);
            });
        }

        // Mettre � jour l'ordre des objets enfants dans la hi�rarchie
        for (int i = 0; i < entryExitObjects.Count; i++)
        {
            entryExitObjects[i].transform.SetSiblingIndex(i);
        }
    }


    }
