using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ingredient ingredient;
    private Vector2 startPosition;
    private Transform parentToReturnTo = null;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("IngredientUI must be a child of a Canvas.");
        }
        else
        {
            Debug.Log("Canvas found: " + canvas.name);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.localPosition;
        parentToReturnTo = transform.parent;
        transform.SetParent(canvas.transform); // Move to the root of the Canvas to avoid clipping issues

        if (parentToReturnTo.TryGetComponent<DropZoneUI>(out var dropZone))
        {
            dropZone.RemoveIngredient(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsInDropZone(eventData))
        {
            transform.SetParent(canvas.transform);
        }
    }

    private bool IsInDropZone(PointerEventData eventData)
    {
        DropZoneUI[] dropZones = FindObjectsOfType<DropZoneUI>();
        Vector2 localMousePosition;
        RectTransform canvasRectTransform = canvas.transform as RectTransform;

        foreach (DropZoneUI zone in dropZones)
        {
            RectTransform dropZoneRectTransform = zone.GetComponent<RectTransform>();

            // Converting screen point to the local point within the drop zone's RectTransform
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localMousePosition))
            {
                // Debug.Log($"Checking drop zone: {zone.name}");
                // Debug.Log($"Local Mouse Position in Drop Zone: {localMousePosition}");
                if (RectTransformUtility.RectangleContainsScreenPoint(zone.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera))
                {
                    Debug.Log($"Ingredient dropped in drop zone: {zone.name}");
                    zone.AddIngredient(gameObject);
                    return true;
                }
            }
        }
        Debug.Log("Ingredient not in any drop zone.");
        return false;
    }
}
