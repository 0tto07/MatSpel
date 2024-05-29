using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class Mainmanu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject tutorial; 

    [SerializeField] private TMP_Text volumeTextValue = null;
    // Called when we click the "Play" button.
    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        SceneManager.LoadScene(1);
    }
    public void CreditsButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void TutorialButton() 
    {
        MainMenu.SetActive(true);
        tutorial.SetActive(true); 
    }
    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    // Called when we click the "Quit" button.
    public void OnQuitButton()
    {
        Application.Quit();
    }
    // Volume setting 
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    
}
