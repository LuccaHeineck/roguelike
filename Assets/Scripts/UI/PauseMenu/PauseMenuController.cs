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
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void OpenSettings()
    {
        Debug.Log("Configurações");
    }

    public void QuitToMenu()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.QuitToMainMenu();
    }
}