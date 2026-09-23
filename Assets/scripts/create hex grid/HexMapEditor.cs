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


    public void SelectColor(int index)
    {
        activeColor = colors[index];
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
            EditCell(hexGrid.GetCell(hit.point));
        }
    }

    void EditCell(HexCell cell)
    {
        cell.Color = activeColor;
        //cell.Elevation = activeElevation;
    }

}