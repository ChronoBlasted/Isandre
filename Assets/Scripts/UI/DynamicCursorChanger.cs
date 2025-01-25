using UnityEngine;
using UnityEngine.EventSystems; // Nécessaire pour détecter les événements UI

public class DynamicCursorChangerWithUI : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    public Texture2D enemyCursor;
    public Texture2D buttonCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    void Update()
    {
        if (IsPointerOverUIElement())
        {
            Cursor.SetCursor(buttonCursor, cursorHotspot, CursorMode.Auto);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.tag)
            {
                case "Interactable":
                    Cursor.SetCursor(hoverCursor, cursorHotspot, CursorMode.Auto);
                    return;
                case "Enemy":
                    Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                    return;
                default:
                    Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
                    return;
            }
        }

        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
