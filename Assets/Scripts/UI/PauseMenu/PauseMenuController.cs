using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        GlobalUIBlurController.SetBlurActive(this, true);
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        GlobalUIBlurController.SetBlurActive(this, false);
        GameManager.Instance.ResumeGame();
    }

    public void OpenSettings()
    {
        Debug.Log("Configurações");
    }

    public void QuitToMenu()
    {
        pausePanel.SetActive(false);
        GlobalUIBlurController.SetBlurActive(this, false);
        GameManager.Instance.QuitToMainMenu();
    }

    private void OnDisable()
    {
        GlobalUIBlurController.SetBlurActive(this, false);
    }
}