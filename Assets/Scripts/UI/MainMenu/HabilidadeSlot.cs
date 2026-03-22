using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HabilidadeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referências")]
    public Image iconeImage;
    public GameObject tooltipObj;
    public TextMeshProUGUI tooltipText;

    private HabilidadeData dadosAtuais;
    private RectTransform tooltipRt;

    void Awake()
    {
        tooltipRt = tooltipObj.GetComponent<RectTransform>();
    }

    public void Preencher(HabilidadeData dados)
    {
        dadosAtuais = dados;
        gameObject.SetActive(true);
        if (dados.icone != null)
            iconeImage.sprite = dados.icone;
    }

    public void Limpar()
    {
        dadosAtuais = null;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dadosAtuais == null) return;

        // Define o texto
        tooltipText.text = $"<b>{dadosAtuais.nomeHabilidade}</b>\n{dadosAtuais.descricao}";

        // Define largura fixa e calcula altura necessária
        float largura = 250f;
        float altura = tooltipText.GetPreferredValues(
            tooltipText.text, largura, 0).y + 20f;

        tooltipRt.sizeDelta = new Vector2(largura, altura);

        tooltipObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObj.SetActive(false);
    }
}