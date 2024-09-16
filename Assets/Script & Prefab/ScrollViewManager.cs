using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    public GameObject entryPrefab; // Le prefab pour une entr�e/sortie
    public Transform contentPanel; // Le panneau o� les �l�ments seront ajout�s
    public List<GameObject> AllEntryExit; 
    // Exemple de m�thode pour ajouter une nouvelle entr�e/sortie
    

    // Exemples de test pour ajouter des entr�es
    void Start()
    {
        
    }


    public void AddEntry(string name, float money, string account, DateTime date, string category)
    {
        GameObject newEntry = Instantiate(entryPrefab, contentPanel);
        newEntry.GetComponent<EntryExit>().Title = name;
        newEntry.GetComponent<EntryExit>().Money = money;
        newEntry.GetComponent<EntryExit>().Account = account;
        newEntry.GetComponent<EntryExit>().Date = date;
        newEntry.GetComponent<EntryExit>().Category = category;
        AllEntryExit.Add(newEntry); 
    }
    public void SetActiveEntryExit()
    {
        foreach (GameObject entryExit in AllEntryExit)
        {
            entryExit.gameObject.SetActive(true);
        }
    }
}