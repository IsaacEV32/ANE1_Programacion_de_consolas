using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InvokePauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    bool isGamePaused = false;
    public static InvokePauseMenu instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus && !pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            isGamePaused = true;
            EventSystem.current.SetSelectedGameObject(pauseMenuUI.GetComponentInChildren<Button>().gameObject);
        }
    }
    public bool IsGamePaused()
    {
        return isGamePaused;
    }
    public void SetGamePaused(bool isPaused)
    {
        isGamePaused = isPaused;
    }
    public void CallPauseMenu(InputAction.CallbackContext pauseMenuContext)
    {
        if (pauseMenuContext.performed && !pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            isGamePaused = true;
            EventSystem.current.SetSelectedGameObject(pauseMenuUI.GetComponentInChildren<Button>().gameObject);
        }
        else if (pauseMenuContext.performed && pauseMenuUI.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1f;
            isGamePaused = false;
            pauseMenuUI.SetActive(false);
        }
    }
}
