using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private List<UIBase> openedUI = new List<UIBase>();
    public void TestUIOption()
    {
        var ui = UIManager.ShowUI<UIOption>();
        ui.Initialize(null);
        openedUI.Add(ui);
    }
    public void CloseUI()
    {
        UIManager.HideTopUI();
        //UIManager.HideUI(openedUI[openedUI.Count - 1]);
    }
}
