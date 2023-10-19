using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Stages
{
    Stage1,
    Stage2,
    Stage3
}
public class StageSelectDoor : MonoBehaviour
{
    public Stages Stage;
    private static readonly string player = "Player";
    private static readonly string Stage1 = "PlayerTestScene";
    private static readonly string Stage2 = "WaterScene";
    private static readonly string Stage3 = "ObjectTest";
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == player)
        {
            string sceneName = string.Empty;
            switch (Stage)
            {
                case Stages.Stage1 : sceneName = Stage1;
                    break;
                case Stages.Stage2 : sceneName = Stage2;
                    break;
                case Stages.Stage3 : sceneName = Stage3;
                    break;
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
