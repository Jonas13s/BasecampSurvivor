using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{

    public static ProgressBars Instance;

    public Image EnergyBar;
    public Image HungryBar;
    public Image DriveBar;
    public Image SocialBar;
    public Image SkillBar;
    
    public CanvasGroup ProjectScreen;

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
        SetProgressBars();

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    public void SetProgressBars()
    {
        FillProgressBar(EnergyBar, (float)Bars.Instance.Energy / (float)100);
        FillProgressBar(HungryBar, (float)Bars.Instance.Hungry / (float)100);
        FillProgressBar(DriveBar, (float)Bars.Instance.Drive / (float)100);
        FillProgressBar(SocialBar, (float)Bars.Instance.Social / (float)100);
        FillProgressBar(SkillBar, (float)Bars.Instance.Skillset / (float)100);
    }

    void FillProgressBar(Image s, float volume)
    {
        s.fillAmount = volume;
    }
}
