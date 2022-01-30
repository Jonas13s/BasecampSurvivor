using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survivor : MonoBehaviour
{
    public Text HungryText;
    public Text DriveText;
    public Text SocialLText;
    public Text SkillText;
    public Text EnergyText;
    public Text TimeText;
    public Text DayText;

    public static Survivor Instance;

    public CanvasGroup NotificationGroup;
    public CanvasGroup ButtonsGroup;
    public Text NotificationText;
    public float NotificationtimeRemaining = 0;
    public bool NotificationBar = false;

     private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Initialize values
        //ResetValues();

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    public void Start()
    {
        TurnCanvas(NotificationGroup, false);
        ProjectTable.Instance.Spawn(0, false);
    }

    // Update is called once per frame
    public void Update()
    {
        HungryText.text = "Hungry: " + Bars.Instance.Hungry + "/100";
        DriveText.text = "Drive: " + Bars.Instance.Drive + "/100";
        SocialLText.text = "Social Life: " + Bars.Instance.Social + "/100";
        EnergyText.text = "Energy: " + Bars.Instance.Energy + "/100";
        SkillText.text = "Knowledge: " + Bars.Instance.Skillset + "/100";

        if (Bars.Instance.Hour >= 24)
        {
            Bars.Instance.Hour = 0;
            Bars.Instance.Day++;
            Bars.Instance.ProjectCount = 1;
            ProjectTable.Instance.Spawn(Bars.Instance.ProjectCount, true);
        }
        if (Bars.Instance.Day <= 9)
            DayText.text = "2022-01-" + "0" + Bars.Instance.Day;
         else
              DayText.text = "2022-01-" + Bars.Instance.Day;
        if (Bars.Instance.Hour < 10)
            TimeText.text = "0" + Bars.Instance.Hour + ":" + "00";
        else if (Bars.Instance.Hour >= 10)
            TimeText.text = Bars.Instance.Hour + ":" + "00";
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
        ProgressBars.Instance.SetProgressBars();
        ForcedActions();
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
        if (Bars.Instance.Energy == 100)
        {
            NotificationPopUp("Why would you want to sleep? You are fully awake and full of energy!", 5);
            return ;
        }
        AdditionalChanges.Instance.AdditionalGroups("Sleep choose" , "Sleep for 8 hours and gain " + Bars.Instance.Multipliers("Sleep") + " Energy" , "Sleep for 4 hours and gain " + (Bars.Instance.Multipliers("Sleep") / 2) + " Energy", "Sleep");
    }
    public void ClickEat()
    {
        if (Bars.Instance.Hungry == 100)
        {
            NotificationPopUp("Why would you want to eat? You are full", 5);
            return ;
        }
        AdditionalChanges.Instance.AdditionalGroups("Food menu", "Buy some snacks, and soda from supermarket. It will take you around 1 hour and you will get 20 Food", "You make yourself three piece meal. It will take you around 2 hours and you will get 55 Food", "Food");

    }
    public void ReturnAdditional(int type, string s)
    {
        if (type == 3)
        {
            TurnCanvas(AdditionalChanges.Instance.AdditionalChangesGroup, false);
            TurnCanvas(ButtonsGroup, true);
        }
        if (s == "Sleep")
        {
            if (type == 1)
            {
                Bars.Instance.AddDelta();
                Bars.Instance.ResetDelta();
                Bars.Instance.LoseDelta(8, "Sleep");
                ReturnAdditional(3, "");
            }
            if (type == 2)
            {
                Bars.Instance.lastDelta.EnergyRandom = Bars.Instance.lastDelta.EnergyRandom / 2;
                Bars.Instance.AddDelta();
                Bars.Instance.ResetDelta();
                Bars.Instance.LoseDelta(4, "Sleep");
                ReturnAdditional(3, "");
            }
        }
        if (s == "Food")
        {
            if (type == 1)
            {
                // what happens when you choose first option
            }
            if (type == 2)
            {
                //what happens when you choose second option
            }

        }
        AdditionalChanges.Instance.CurrentAddition = "";
    }

    public void NotificationPopUp(string s, int timeS)
    {
        NotificationText.text = s;
        NotificationBar = true;
        NotificationtimeRemaining = timeS;
        TurnCanvas(NotificationGroup, true);
    }

    public void ForcedActions()
    {
        if (Bars.Instance.Energy <= 0 + Random.Range(0, 5))
        {
            NotificationPopUp("You fell asleep while being at your desk for 4 hours...", 10);
            Bars.Instance.Multipliers("Sleep");
            ReturnAdditional(2, "Sleep");
        }
        if (Bars.Instance.Hungry <= 0 + Random.Range(0, 10))
        {
            NotificationPopUp("You haven't eaten anything you feel bad... you lost energy and drive", 10);
            Bars.Instance.Energy -= 10;
            Bars.Instance.Drive -= 15;
        }
    }
}
