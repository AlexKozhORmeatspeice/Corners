using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;

public class MoveWithJumpVert : IMovableFigure
{
    private Transform figureTransform;
    private float speed;
    private float distToMove;
    
    public MoveWithJumpVert(Transform figureTransform, float speed)
    {
        distToMove = CellController.Instance.DistBetweenCells * 1.5f;
        
        this.figureTransform = figureTransform;
        this.speed = speed;
    }
    
    public void Move(Vector3 target)
    {
        Vector3 direction = target - figureTransform.position;
        
        RaycastHit2D hit = Physics2D.Raycast(figureTransform.position ,direction);
        
        Debug.Log(direction);

        if (Vector2.Distance(figureTransform.position, target) <= distToMove)
        {
            figureTransform.position = target;
        }
        
        
    }
}
