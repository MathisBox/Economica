using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EntryExit : MonoBehaviour
{
    public float Money;
    public string Title;
    public string Account;
    public DateTime Date;
    public string Category;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI AccountText;
    public TextMeshProUGUI DateText;
    public Image Background;
    public Image CategoryIMG;
    

    private void Start()
    {
        DateText.text = Date.ToShortDateString();
        Name.text = Title.ToString();
        MoneyText.text = Money.ToString();
        AccountText.text = Account;
        CategoryIMG.sprite = Resources.Load<Sprite>(Category);
        if (Money < 0)
        {
            Background.color = Color.red;
        }
        else
        {
            Background.color= Color.green;
            MoneyText.text = "+ " + MoneyText.text;
        }

    }


}
