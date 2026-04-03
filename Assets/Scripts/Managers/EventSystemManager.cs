using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class EventSystemManager : MonoBehaviour
{
    private static EventSystemManager instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        EnsureSingletonExists();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureRequiredComponents(gameObject);
        DisableDuplicateEventSystems();

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (instance != this)
            return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        instance = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DisableDuplicateEventSystems();
    }

    private static void EnsureSingletonExists()
    {
        if (instance != null)
            return;

        EventSystemManager existingManager = FindFirstObjectByType<EventSystemManager>();
        if (existingManager != null)
        {
            EnsureRequiredComponents(existingManager.gameObject);
            return;
        }

        EventSystem existingSystem = FindFirstObjectByType<EventSystem>();
        if (existingSystem != null)
        {
            EnsureRequiredComponents(existingSystem.gameObject);
            if (existingSystem.GetComponent<EventSystemManager>() == null)
                existingSystem.gameObject.AddComponent<EventSystemManager>();
            return;
        }

        GameObject go = new GameObject("EventSystem");
        EnsureRequiredComponents(go);
        go.AddComponent<EventSystemManager>();
    }

    private static void EnsureRequiredComponents(GameObject go)
    {
        if (go.GetComponent<EventSystem>() == null)
            go.AddComponent<EventSystem>();

        if (go.GetComponent<InputSystemUIInputModule>() == null)
            go.AddComponent<InputSystemUIInputModule>();
    }

    private void DisableDuplicateEventSystems()
    {
        EventSystem ownEventSystem = GetComponent<EventSystem>();
        EventSystem[] systems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

        for (int i = 0; i < systems.Length; i++)
        {
            EventSystem system = systems[i];
            if (system == null || system == ownEventSystem)
                continue;

            system.enabled = false;

            InputSystemUIInputModule inputModule = system.GetComponent<InputSystemUIInputModule>();
            if (inputModule != null)
                inputModule.enabled = false;

            StandaloneInputModule legacyModule = system.GetComponent<StandaloneInputModule>();
            if (legacyModule != null)
                legacyModule.enabled = false;
        }

        if (ownEventSystem != null)
            ownEventSystem.enabled = true;

        InputSystemUIInputModule ownInputModule = GetComponent<InputSystemUIInputModule>();
        if (ownInputModule != null)
            ownInputModule.enabled = true;
    }
}