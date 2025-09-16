using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public string _mainMenuScene = "MainMenu";    

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }

    public void TurnOnSound()
    {
        Debug.Log("Sound is On!");
    }

    public void TurnOffSound()
    {
        Debug.Log("Sound is Off!");
    }

}
