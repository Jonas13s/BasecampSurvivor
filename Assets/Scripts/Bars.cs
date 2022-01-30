using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BarDelta
{
    public int Hour;
    public int Energy;
    public int EnergyRandom;
    public int Hungry;
    public int HungryRandom;
    public int Drive;
    public int DriveRandom;
    public int Social;
    public int SocialRandom;
    public int Work;
    public int WorkRandom;
    public int SkillsetRandom;

    public bool IsEmpty()
    {
        if (Hour == 0 && Energy == 0 && Hungry == 0 && Drive == 0 && Social == 0 && Work == 0) return true;
        return false;
    }
}
public class Bars : MonoBehaviour
{
    // Singleton
    public static Bars Instance;
    public int Energy;
    public int Hungry;
    public int Drive;
    public int Social;
    public int Work;
    public int Hour;
    public int Day;
    public int ProjectCount;
    public int Skillset;

    public BarDelta lastDelta;

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
        ResetValues();
        ResetDelta();

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    public void ResetValues()
    {
        Hungry = 100;
        Drive = 100;
        Social = 100;
        Energy = 100;
        Work = 0;
        Hour = 8;
        Day = 0;
        ProjectCount = 1;
        Skillset = 0;
        //Lastdata
    }

    public string GetStatusString()
    {
        return $"Hungry {Hungry}/100 Drive {Drive}/100 Social {Social}/100 Energy {Energy}/100";
    }

    public string GetDeltaString()
    {
        if (lastDelta.IsEmpty()) return "";
        string returnString = $"Spent {lastDelta.Hour} Hours to get ";
        if (lastDelta.Hungry != 0) returnString += $"Hungry [{lastDelta.Hungry}] ";
        if (lastDelta.Drive != 0) returnString += $"Drive [{lastDelta.Drive}] "; 
        if (lastDelta.Social != 0) returnString += $"Social [{lastDelta.Social}] ";
        if (lastDelta.Energy != 0) returnString += $"Energy [{lastDelta.Energy}]";
        return returnString;
    }

    public int Multipliers(string type)
    {
        float multiplier = 1;
        int result;
        if (type == "Sleep")
        {
            if (Hungry >= Random.Range(60, 100))
                multiplier += (float)0.23;
            else if (Hungry >= Random.Range(40, 60))
                multiplier += (float)0.11;
            else if (Hungry <= Random.Range(10, 40))
                multiplier -= (float)0.17;
            else if (Hungry <= Random.Range(0, 10))
                multiplier -= (float)0.24;

            if (Social >= Random.Range(60, 100))
                multiplier += (float)0.15;
            else if (Social >= Random.Range(40, 60))
                multiplier += (float)0.09;
            else if (Social <= Random.Range(10, 40))
                multiplier -=(float) 0.06;
            else if (Social <= Random.Range(0, 10))
                multiplier -= (float)0.16;
            if (Energy > 0)
            {
                result = (int)multiplier * Random.Range(50, 100);
                if (result > 100)
                    result = 100;
                if (lastDelta.EnergyRandom > result || lastDelta.EnergyRandom == 0)
                    lastDelta.EnergyRandom = result;
                else
                    return (lastDelta.EnergyRandom);
            }
            else
            {
                result = (int)multiplier * Random.Range(1, 50);
                if (lastDelta.EnergyRandom > result || lastDelta.EnergyRandom == 0)
                    lastDelta.EnergyRandom = result;
                else
                    return (lastDelta.EnergyRandom);
            }
            return (lastDelta.EnergyRandom);
        }
        return (0);
        
    }

    public void ResetDelta()
    {
        lastDelta.Energy = 0;
        lastDelta.EnergyRandom = 0;
        lastDelta.Hour = 0;
        lastDelta.Hungry = 0;
        lastDelta.HungryRandom = 0;
        lastDelta.Drive = 0;
        lastDelta.DriveRandom = 0;
        lastDelta.Social = 0;
        lastDelta.SocialRandom = 0;
        lastDelta.Work = 0;
        lastDelta.WorkRandom = 0;
        lastDelta.SkillsetRandom = 0;
    }

    public void AddDelta()
    {
        // Random.Range Docs state that if second parameter is less than first parameter, it will work, but just swap their places
        Energy += lastDelta.EnergyRandom;
        if (Energy > 100) Energy = 100;

        lastDelta.Hungry += Random.Range(0, lastDelta.HungryRandom);
        Hungry += lastDelta.Hungry;
        if (Hungry > 100) Hungry = 100;

        lastDelta.Drive += Random.Range(0, lastDelta.DriveRandom);
        Drive += lastDelta.Drive;
        if (Drive > 100) Drive = 100;

        lastDelta.Social += Random.Range(0, lastDelta.SocialRandom);
        Social += lastDelta.Social;
        if (Social > 100) Social = 100;

        lastDelta.Work += Random.Range(0, lastDelta.WorkRandom);
        Work += lastDelta.Work;
        if (Work > 100) Work = 100;
        
        // No Random on Hour, minimum unit is too small for that I think!
        Hour += lastDelta.Hour;
    }
    public void LoseDelta(int count, string action)
    {
        
        if (action != "Sleep")
        {
            Energy -= count * 4;
            if (Energy < 0) Energy = 0;

            Social -= count * (int)1.5;
            if (Social < 0) Social = 0;
        }
        Hungry -= count * 5;
        if (Hungry < 0) Hungry = 0;
        
    }
}
