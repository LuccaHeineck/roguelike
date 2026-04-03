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

    [Header("Ícone padrão para habilidades bloqueadas")]
    public Sprite iconeLockedPadrao; // arraste um sprite de cadeado aqui

    private HabilidadeData dadosAtuais;
    private RectTransform tooltipRt;

    void Awake()
{
    if (iconeImage == null)
    {
        Transform iconeTransform = transform.Find("Icone");
        if (iconeTransform != null)
            iconeImage = iconeTransform.GetComponent<Image>();
    }

    if (tooltipObj != null)
        tooltipRt = tooltipObj.GetComponent<RectTransform>();
}

public void Preencher(HabilidadeData dados)
{
    dadosAtuais = dados;
    gameObject.SetActive(true);

    if (iconeImage == null) return;

    if (dados.isLocked)
    {
        // Ícone genérico cinza
        iconeImage.color = new Color(0.4f, 0.4f, 0.4f, 1f);
        iconeImage.sprite = null;
    }
    else
    {
        // Ícone normal branco
        iconeImage.color = Color.white;

        if (dados.icone != null)
            iconeImage.sprite = dados.icone;
    }
}

    public void Limpar()
    {
        dadosAtuais = null;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dadosAtuais == null) return;

        // Muda o texto do tooltip dependendo do estado
        if (dadosAtuais.isLocked)
            tooltipText.text = $"<b>???</b>\n{dadosAtuais.descricaoLocked}";
        else
            tooltipText.text = $"<b>{dadosAtuais.nomeHabilidade}</b>\n{dadosAtuais.descricao}";

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