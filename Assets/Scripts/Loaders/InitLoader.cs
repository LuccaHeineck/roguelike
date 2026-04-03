using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}