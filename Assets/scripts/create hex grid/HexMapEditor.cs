using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{

    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;


    InputAction clickAction;
    InputAction mousePosAction;

    bool applyColor;

    int brushSize;

    public void SelectColor(int index)
    {
        applyColor = index >= 0;
        if (applyColor)
        {
            activeColor = colors[index];
        }
    }



    void Awake()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        mousePosAction = InputSystem.actions.FindAction("MousePos");

        SelectColor(0);
    }


    void Update()
    {
        if (clickAction.IsPressed() && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }

    }


    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(mousePosAction.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            EditCells(hexGrid.GetCell(hit.point));
        }
    }

    void EditCells(HexCell center)
    {
        int centerX = center.coordinates.X;
        int centerZ = center.coordinates.Z;

        for (int r = 0, z = centerZ - brushSize; z <= centerZ; z++, r++) /////// top half of brush
        {
            for (int x = centerX - r; x <= centerX + brushSize; x++)
            {
                EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
            }
        }
        for (int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++) /////// bottom half of brush
        {
            for (int x = centerX - brushSize; x <= centerX + r; x++)
            {
                EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
            }
        }
    }

    void EditCell(HexCell cell)
    {
        if (cell)
        {
            if (applyColor)
            {
            cell.Color = activeColor;
            }
        }
    }

    public void SetBrushSize(float size)
    {
        brushSize = (int)size;
    }

    public void ShowUI(bool visible)
    {
        hexGrid.ShowUI(visible);
    }

}