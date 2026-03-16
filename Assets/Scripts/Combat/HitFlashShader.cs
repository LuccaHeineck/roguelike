using UnityEngine;
using System.Collections;

public class HitFlashShader : MonoBehaviour
{
    [SerializeField] private float flashTime = 0.1f;
    private Material mat;

    void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        mat.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(flashTime);
        mat.SetFloat("_FlashAmount", 0f);
    }
}