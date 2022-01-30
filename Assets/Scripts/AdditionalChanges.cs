using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalChanges : MonoBehaviour
{
    public static AdditionalChanges Instance;

    public CanvasGroup AdditionalChangesGroup;
    public Text OptionAText;
    public Text OptionBText;
    public Text TitleText;
    public string CurrentAddition;

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
        //SetProgressBars();
        CurrentAddition = "";

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    public void ClickOption(int type)
    {
        Survivor.Instance.ReturnAdditional(type, CurrentAddition);
    }
    public void AdditionalGroups(string Title, string A, string B, string s)
    {
        TitleText.text = Title;
        OptionAText.text = A;
        OptionBText.text = B;
        CurrentAddition = s;
        Survivor.Instance.TurnCanvas(Survivor.Instance.ButtonsGroup, false);
        Survivor.Instance.TurnCanvas(AdditionalChangesGroup, true);
    }
}
