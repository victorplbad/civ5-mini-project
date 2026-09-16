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
            hexGrid.ColorCell(hit.point, activeColor);
        }
    }

    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}