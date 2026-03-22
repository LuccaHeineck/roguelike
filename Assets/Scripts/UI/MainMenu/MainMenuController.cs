using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void BotaoNovoJogo()
    {
        SceneManager.LoadScene("FactionSelect");
    }

    public void BotaoSair()
    {
        Application.Quit();
        Debug.Log("Quit não funciona em editor, então por enquanto esse log representa OK");
    }
}