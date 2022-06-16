using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class MoveAction : NetworkBehaviour
{
    private Unit selectedUnit;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    public void SetUp(Unit unit)
    {
        //this.unit = unit;
    }

    private void Update()
    {

        //transform.position +=
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
