using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipFollowMouse : MonoBehaviour
{
    private RectTransform rt;
    private RectTransform canvasRt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvasRt = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRt,
            mousePos,
            null,
            out pos
        );
        rt.localPosition = pos + new Vector2(15, -15);
    }
}