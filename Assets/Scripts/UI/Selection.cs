using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    public void StartBtn()
    {
        Debug.Log("Press Start Button");
        SceneManager.LoadScene(1);
    }

    public void OptionBtn()
    {
        Debug.Log("Press Option Button");
        gameObject.SetActive(false);
        var ui = UIManager.ShowUI<UIOption>();
        ui.Initialize(() => { gameObject.SetActive(true); });
    }

    public void ExitBtn()
    {
        Debug.Log("Press Exit Button");
        Application.Quit();
    }
}
