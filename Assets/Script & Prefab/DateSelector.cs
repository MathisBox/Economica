using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DateSelector : MonoBehaviour
{
    public AddEntryExit addentryexit;

    public GameObject calendarPanel; // Panel qui contient le calendrier
    public TextMeshProUGUI monthYearText; // Affiche le mois et l'année
    public Button dayButtonPrefab; // Préfab pour chaque bouton de jour
    public Transform daysContainer; // Conteneur pour les boutons des jours

    private DateTime currentDate; // Date actuellement affichée dans le calendrier
    private DateTime selectedDate; // Date sélectionnée par l'utilisateur

    void Start()
    {
        // Initialiser avec la date actuelle
        currentDate = DateTime.Now;
        selectedDate = DateTime.Now;
        UpdateCalendar();

    }

    public void ShowCalendar()
    {
        calendarPanel.SetActive(true);
        UpdateCalendar();
    }

    public void HideCalendar()
    {
        calendarPanel.SetActive(false);
    }

    public void OnNextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        UpdateCalendar();
    }

    public void OnPreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        UpdateCalendar();
    }

    void UpdateCalendar()
    {
        // Effacer les anciens boutons de jour
        foreach (Transform child in daysContainer)
        {
            Destroy(child.gameObject);
        }

        // Mettre à jour le texte du mois et de l'année
        monthYearText.text = currentDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

        // Obtenir le premier jour du mois
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

        // Remplir le calendrier avec des boutons de jour
        for (int day = 1; day <= daysInMonth; day++)
        {
            DateTime date = new DateTime(currentDate.Year, currentDate.Month, day);
            Button dayButton = Instantiate(dayButtonPrefab, daysContainer);
            dayButton.GetComponentInChildren<TextMeshProUGUI>().text = day.ToString();
            dayButton.onClick.AddListener(() => OnDaySelected(date));
        }
    }

    void OnDaySelected(DateTime date)
    {
        selectedDate = date;
        addentryexit.UpdateDate(date);
        HideCalendar();
        


    }
}
