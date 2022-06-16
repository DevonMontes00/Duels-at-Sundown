using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridCreation;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private GameController gc;
    [SerializeField] private Camera mainCamera;
    [SerializeField] public GridXZ<GridObject> grid;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }

        if(Input.GetMouseButtonDown(0))
        {
            //grid.GetXZ(transform.position, out int x, out int z);
           // gc.selectedUnit.transform.position = grid.GetWorldPosition(x, z);
        }

    }
}
