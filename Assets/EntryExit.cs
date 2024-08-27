using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntryExit : MonoBehaviour
{
    public float Money;
    public string Title;
    public string Account;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI AccountText;
    public Image Background;

    private void Start()
    {
        Name.text = Title.ToString();
        MoneyText.text = Money.ToString();
        AccountText.text = Account;
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
