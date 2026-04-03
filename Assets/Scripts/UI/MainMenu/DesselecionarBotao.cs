using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesselecionarBotao : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}