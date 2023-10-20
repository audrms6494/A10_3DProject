using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    public string dragonName;
    [SerializeField] private PlayerInput playerInput;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            var ui = UIManager.ShowUI<UIGameClear>();
            ui.Initalize(playerInput, dragonName);
            string sceneName = SceneManager.GetActiveScene().name;
            switch (sceneName)
            {
                case "PlayerTestScene":
                    GameManager.Instance.StageClearFlags[0] = true;
                    break;
                case "WaterScene":
                    GameManager.Instance.StageClearFlags[1] = true;
                    break;
                case "ObjectTest":
                    GameManager.Instance.StageClearFlags[2] = true;
                    break;
            }
        }
    }
}
