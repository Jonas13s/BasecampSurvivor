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

    public ProjectTable ProjectScript;
    public Bars BarsData;

    public void Start()
    {
        TurnCanvas(NotificationGroup, false);
        ProjectScript.Spawn(0, false);
    }

    // Update is called once per frame
    public void Update()
    {
        HungryText.text = "Hungry: " + BarsData.Hungry + "/100";
        SleepText.text = "Sleep: " + BarsData.Sleep + "/100";
        DriveText.text = "Drive: " + BarsData.Drive + "/100";
        SocialLText.text = "Social Life: " + BarsData.Social + "/100";
        WorkText.text = "Work done: " + BarsData.Work + "/100";
        EnergyText.text = "Energy: " + BarsData.Energy + "/100";
        if (BarsData.Hour >= 24)
        {
            BarsData.Hour = 0;
            BarsData.Day++;
            BarsData.ProjectCount = 1;
            ProjectScript.Spawn(BarsData.ProjectCount, true);
        }
        if (BarsData.Day <= 9)
            DayText.text = "2022-01-" + "0" + BarsData.Day;
         else
              DayText.text = "2022-01-" + BarsData.Day;
        if (BarsData.Hour < 10)
            TimeText.text = "0" + BarsData.Hour + ":" + "00";
        else if (BarsData.Hour >= 10)
            TimeText.text = BarsData.Hour + ":" + "00";
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
        BarsData.AddDelta();
        /*
        if (Energy >= 15)
        {
            if (Work + 10 <= 100)
            {
                Work += 10;
                Hungry -= 5;
                Energy -= 15;
                Hour++;
                ProjectScript.Spawn(ProjectCount, false);
                ProjectCount++;
            }
        }*/
    }
    public void ClickSleep()
    {
        SleepChoose(1);
    }
    public void SleepChoose(int a)
    {/*
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
        }*/
    }
    public void NotificationPopUp(string s, int timeS)
    {
        NotificationText.text = s;
        NotificationBar = true;
        NotificationtimeRemaining = timeS;
        TurnCanvas(NotificationGroup, true);
    }
}
