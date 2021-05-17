using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;

public class MoveOnOneCell : IMovableFigure
{
    private Transform figureTransform;
    private float speed;
    private float distToMove;
    
    public MoveOnOneCell(Transform figureTransform, float speed)
    {
        distToMove = CellController.Instance.DistBetweenCells * 1.5f;
        
        this.figureTransform = figureTransform;
        this.speed = speed;
    }
    
    public void Move(Vector3 target)
    {
        target.z -= 1.0f;
        if (Vector2.Distance(figureTransform.position, target) <= distToMove)
        {
            figureTransform.position = target;
        }
    }

}
