using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survivor : MonoBehaviour
{
    public Text HungryText;
    public Text SleepText;
    public Text DriveText;
    public Text SocialLText;
    public Text WorkText;
    public Text EnergyText;
    public Text TimeText;
    public Text DayText;

    public CanvasGroup NotificationGroup;
    public Text NotificationText;
    public float NotificationtimeRemaining = 0;
    public bool NotificationBar = false;

    public int Energy;
    public int Hungry;
    public int Sleep;
    public int Drive;
    public int SocialL;
    public int Work;
    public int Hour;
    public int Day;

    public void Start()
    {
        TurnCanvas(NotificationGroup, false);
        Hungry = 100;
        Sleep = 100;
        Drive = 100;
        SocialL = 100;
        Energy = 100;
        Work = 0;
        Hour = 8;
        Day = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        HungryText.text = "Hungry: " + Hungry + "/100";
        SleepText.text = "Sleep: " + Sleep + "/100";
        DriveText.text = "Drive: " + Drive + "/100";
        SocialLText.text = "Social Life: " + SocialL + "/100";
        WorkText.text = "Work done: " + Work + "/100";
        EnergyText.text = "Energy: " + Energy + "/100";
        if (Hour >= 24)
        {
            Hour = 0;
            Day++;
        }
        if (Day <= 9)
            DayText.text = "2022-01-" + "0" + Day;
         else
              DayText.text = "2022-01-" + Day;
        if (Hour < 10)
            TimeText.text = "0" + Hour + ":" + "00";
        else if (Hour >= 10)
            TimeText.text = Hour + ":" + "00";
        //notification bar//
        if (NotificationBar)
        {
            if (NotificationtimeRemaining > 0)
                NotificationtimeRemaining -= Time.deltaTime;
            else
            {
                NotificationtimeRemaining = 0;
                NotificationBar = false;
                TurnCanvas(NotificationGroup, false);
            }
        }
        //
    }
    public void TurnCanvas(CanvasGroup name, bool on)
    {
        if(on)
        {
            name.alpha = 1;
            name.blocksRaycasts = true;
            name.interactable = true;
        }
        else
        {
            name.alpha = 0;
            name.blocksRaycasts = false;
            name.interactable = false;
        }
    }
    public void ClickWork()
    {
        if (Energy >= 15)
        {
            if (Work + 10 <= 100)
            {
                Work += 10;
                Hungry -= 5;
                Energy -= 15;
                Hour++;
            }
        }
    }
    public void ClickSleep()
    {
        SleepChoose(1);
    }
    public void SleepChoose(int a)
    {
        if (a == 1)
        {
            Hour += 8;
            Energy += 35;
            Sleep += 50;
            Hungry -= Random.Range(20, 40);
            if (Hungry < 0)
            {
                Hungry = 0;
                NotificationPopUp("You slept for 8 hours and got really hungry, eat before it affects you health", 10);
            }
        }
    }
    public void NotificationPopUp(string s, int timeS)
    {
        NotificationText.text = s;
        NotificationBar = true;
        NotificationtimeRemaining = timeS;
        TurnCanvas(NotificationGroup, true);
    }
}
