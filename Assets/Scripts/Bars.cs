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
    public int Sleep;
    public int SleepRandom;
    public int Drive;
    public int DriveRandom;
    public int Social;
    public int SocialRandom;
    public int Work;
    public int WorkRandom;

    public bool IsEmpty()
    {
        if (Hour == 0 && Energy == 0 && Hungry == 0 && Sleep == 0 && Drive == 0 && Social == 0 && Work == 0) return true;
        return false;
    }
}
public class Bars : MonoBehaviour
{
    // Singleton
    public static Bars Instance;
    public int Energy;
    public int Hungry;
    public int Sleep;
    public int Drive;
    public int Social;
    public int Work;
    public int Hour;
    public int Day;

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

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    public void ResetValues()
    {
        Hungry = 100;
        Sleep = 100;
        Drive = 100;
        Social = 100;
        Energy = 100;
        Work = 0;
        Hour = 8;
        Day = 0;
    }

    public string GetStatusString()
    {
        return $"Hungry {Hungry}/100 Sleep {Sleep}/100 Drive {Drive}/100 Social {Social}/100 Energy {Energy}/100";
    }

    public string GetDeltaString()
    {
        if (lastDelta.IsEmpty()) return "";
        string returnString = $"Spent {lastDelta.Hour} Hours to get ";
        if (lastDelta.Hungry != 0) returnString += $"Hungry [{lastDelta.Hungry}] ";
        if (lastDelta.Sleep != 0) returnString += $"Sleep [{lastDelta.Sleep}] ";
        if (lastDelta.Drive != 0) returnString += $"Drive [{lastDelta.Drive}] "; 
        if (lastDelta.Social != 0) returnString += $"Social [{lastDelta.Social}] ";
        if (lastDelta.Energy != 0) returnString += $"Energy [{lastDelta.Energy}]";
        return returnString;
    }

    public void AddDelta()
    {
        // Random.Range Docs state that if second parameter is less than first parameter, it will work, but just swap their places
        lastDelta.Energy += Random.Range(0, lastDelta.EnergyRandom);
        Energy += lastDelta.Energy;
        if (Energy > 100) Energy = 100;

        lastDelta.Hungry += Random.Range(0, lastDelta.HungryRandom);
        Hungry += lastDelta.Hungry;
        if (Hungry > 100) Hungry = 100;

        lastDelta.Sleep += Random.Range(0, lastDelta.SleepRandom);
        Sleep += lastDelta.Sleep;
        if (Sleep > 100) Sleep = 100;

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
}
