using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public string _mainMenuScene = "MainMenu";    

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }
}
