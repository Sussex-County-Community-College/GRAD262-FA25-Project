using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public Animator animator;

    public void SceneFade()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("FadeScene", true);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    public void OptionsView()
    {
        if(mainPanel.activeSelf == true)
        {
            mainPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }
}
