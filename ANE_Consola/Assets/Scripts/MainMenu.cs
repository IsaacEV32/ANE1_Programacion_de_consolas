using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] int levelID;
    [SerializeField] GameObject creditsUI;
    [SerializeField] GameObject mainMenuUI;
    GameObject selectedButton;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelID);
    }
    public void LoadCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        selectedButton = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsUI.GetComponentInChildren<Button>().gameObject);
    }
    public void LoadMainMenu()
    {
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton);
        selectedButton = null;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
