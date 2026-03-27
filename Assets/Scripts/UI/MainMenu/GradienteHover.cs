using UnityEngine;
using UnityEngine.UI;

public class GradienteHover : MonoBehaviour
{
    [Header("Configurações do gradiente")]
    [Range(0.1f, 5f)]
    public float velocidade = 1f;    // quanto maior, mais concentrado no centro

    [Range(0f, 1f)]
    public float intensidade = 1f;   // alpha máximo no centro

    [Range(0f, 1f)]
    public float largura = 1f;       // quanto do botão o gradiente ocupa

    public Color cor = Color.white;  // cor do gradiente

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
        AtualizarGradiente();
    }

    public void AtualizarGradiente()
    {
        Texture2D tex = new Texture2D(256, 1);

        for (int x = 0; x < 256; x++)
        {
            float t = x / 255f;

            // Centraliza e aplica a largura
            float centro = (t - 0.5f) / largura + 0.5f;
            centro = Mathf.Clamp01(centro);

            // Aplica a velocidade na curva do seno
            float alpha = Mathf.Pow(
                Mathf.Sin(centro * Mathf.PI), 
                velocidade  // quanto maior, mais rápido cai nas bordas
            ) * intensidade;

            tex.SetPixel(x, 0, new Color(cor.r, cor.g, cor.b, alpha));
        }

        tex.Apply();

        Sprite sprite = Sprite.Create(tex,
            new Rect(0, 0, 256, 1),
            new Vector2(0.5f, 0.5f));

        img.sprite = sprite;
    }

    // Atualiza em tempo real no Editor ao mudar os valores
    void OnValidate()
    {
        if (img == null)
            img = GetComponent<Image>();
        AtualizarGradiente();
    }
}