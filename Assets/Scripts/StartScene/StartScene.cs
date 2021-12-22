using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button newButton;
    public Button continueButton;
    public string gameSceneName;

    private void Awake()
    {
        newButton.onClick.AddListener(() =>
        {
            if (SaveAndLoadManager.ExistsPath())
            {
                SaveAndLoadManager.DeleteFile();
            }

            SceneManager.LoadScene(gameSceneName);
        });

        if (SaveAndLoadManager.ExistsPath())
        {
            continueButton.interactable = true;

            continueButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(gameSceneName);
            });
        }
    }
}
