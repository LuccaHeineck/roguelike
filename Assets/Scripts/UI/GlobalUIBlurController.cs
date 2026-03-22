using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public class GlobalUIBlurController : MonoBehaviour
{
    public static GlobalUIBlurController Instance { get; private set; }

    [SerializeField] private Volume blurVolume;
    [SerializeField, Range(0f, 1f)] private float activeWeight = 1f;

    private readonly HashSet<int> activeSources = new HashSet<int>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureInstanceAtStartup()
    {
        if (FindFirstObjectByType<GlobalUIBlurController>() != null)
            return;

        var host = new GameObject(nameof(GlobalUIBlurController));
        host.AddComponent<GlobalUIBlurController>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        activeSources.Clear();
        EnsureVolumeReference();
        ApplyWeight();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (Instance == this)
            Instance = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        activeSources.Clear();
        blurVolume = null;
        ApplyWeight();
    }

    public static void SetBlurActive(Object source, bool isActive)
    {
        if (source == null)
            return;

        var controller = GetController();
        if (controller == null)
            return;

        controller.SetBlurInternal(source.GetInstanceID(), isActive);
    }

    private static GlobalUIBlurController GetController()
    {
        return Instance != null ? Instance : FindFirstObjectByType<GlobalUIBlurController>();
    }

    private void SetBlurInternal(int sourceId, bool isActive)
    {
        if (isActive)
            activeSources.Add(sourceId);
        else
            activeSources.Remove(sourceId);

        ApplyWeight();
    }

    private void ApplyWeight()
    {
        EnsureVolumeReference();

        if (blurVolume == null)
            return;

        blurVolume.weight = activeSources.Count > 0 ? activeWeight : 0f;
    }

    private void EnsureVolumeReference()
    {
        if (blurVolume != null)
            return;

        var volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
        Volume best = null;

        for (int i = 0; i < volumes.Length; i++)
        {
            var candidate = volumes[i];
            if (candidate == null || !candidate.enabled)
                continue;

            if (best == null)
            {
                best = candidate;
                continue;
            }

            if (candidate.isGlobal && !best.isGlobal)
            {
                best = candidate;
                continue;
            }

            if (candidate.isGlobal == best.isGlobal && candidate.priority > best.priority)
                best = candidate;
        }

        blurVolume = best;
    }
}
