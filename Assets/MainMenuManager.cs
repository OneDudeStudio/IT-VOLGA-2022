using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _helpPanel;
    public void PlaySingle()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayMultiplayer()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowHelpPanel()
    {
        _helpPanel.SetActive(true);
    }
    public void HideHelpPanel()
    {
        _helpPanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
