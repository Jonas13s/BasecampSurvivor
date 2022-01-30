using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProjectTable : MonoBehaviour
{
    
    public static ProjectTable Instance;

    public ProjectsData ProjectDataValues;
    public Text ProjectTitleText;
    public Text ProjectDescriptionText;

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

    public void Spawn(int day, bool next)
    {
        if (day <= ProjectDataValues.max)
        {
            ProjectTitleText.text = ProjectDataValues.title[day];
            ProjectDescriptionText.text = ProjectDataValues.description[day];
            if (next)
                ProjectDataValues = ProjectDataValues.NextDay;
        }
    }
}
