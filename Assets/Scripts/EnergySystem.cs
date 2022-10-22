using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int maximumEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;

    [Header("Scripts")]
    [SerializeField] private NotificationManager notificationManager;
    [SerializeField] private MainMenu mainMenu;


    private int _energy;
    public int Energy => _energy;

    private const string ENERGY_KEY = "Energy";
    private const string ENERGY_READY_KEY = "EnergyReady";

    private void OnEnable()
    {
        mainMenu.OnPLay += ManageEnergy;
    }

    private void OnDisable()
    {
        mainMenu.OnPLay -= ManageEnergy;
    }

    void Start()
    {
        _energy = PlayerPrefs.GetInt(ENERGY_KEY, maximumEnergy);
        CheckEnergyRecharge();
        UpdateEnergyDisplay();
    }

    private void CheckEnergyRecharge()
    {
        if (_energy != 0) return;
        playButton.interactable = false;
        var energyReadyString = PlayerPrefs.GetString(ENERGY_READY_KEY, string.Empty);
        if (energyReadyString == string.Empty) return;

        DateTime energyReady = DateTime.Parse(energyReadyString);

        if (DateTime.Now > energyReady)
        {
            EnergyRecharged();
        }
        else
        {
            Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
        }
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        _energy = maximumEnergy;
        PlayerPrefs.SetInt(ENERGY_KEY, _energy);
        UpdateEnergyDisplay();
    }

    private void UpdateEnergyDisplay()
    {
        energyText.text = $":{_energy}";
    }

    private void ManageEnergy()
    {
        _energy--;
        PlayerPrefs.SetInt(ENERGY_KEY, _energy);
        
        ScheduleEnergyRecharge();
    }

    private void ScheduleEnergyRecharge()
    {
        if (_energy != 0) return;
        var energyReadyTime = DateTime.Now.AddMinutes(energyRechargeDuration);
        PlayerPrefs.SetString(ENERGY_READY_KEY, energyReadyTime.ToString());
        notificationManager.ScheduleNotification(energyReadyTime);
    }
}
