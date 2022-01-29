using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Projects/ProjectData", order = 1)]
public class ProjectsData : ScriptableObject
{
    public string[] title;

    public int number;
    public string[] description;
    public int max;

    public ProjectsData NextDay;
}