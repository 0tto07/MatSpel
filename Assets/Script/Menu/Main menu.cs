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
    public void PlayNow()
    {
        SceneManager.LoadScene("Gamelevel");
    }
    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
    public void CreditsButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void TutorialButton()
    {
        MainMenu.SetActive(false);
        tutorial.SetActive(true);
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
