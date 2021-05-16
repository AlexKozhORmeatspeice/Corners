using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;

public class MoveWithJumpVertNHoriz : IMovableFigure
{
    private Transform figureTransform;
    private float speed;
    private float distToMove;
    
    public MoveWithJumpVertNHoriz(Transform figureTransform, float speed)
    {
        distToMove = CellController.Instance.DistBetweenCells * 1.5f;
        
        this.figureTransform = figureTransform;
        this.speed = speed;
    }
    
    public void Move(Vector3 target)
    {
        Vector3 direction = target - figureTransform.position;
        direction.z = 0;

        float distance = Vector2.Distance((Vector2) figureTransform.transform.position, target);
        
        if (distance >= distToMove)
        { 
            //vert and horizontal move
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)figureTransform.position, direction, distToMove);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform != figureTransform && hit.transform.GetComponent<Figure>())
                {
                    
                    //vert
                    if (direction.x != 0 && direction.y != 0 && distance <= distToMove * 2)
                    {
                        figureTransform.transform.position = target;
                        break;
                    } 
                    //horizontal
                    if(distance <= distToMove * 1.5f)
                    {
                        figureTransform.transform.position = target;
                        break;
                    }

                }
            }
            
        } 
        else
        {
            //simple move
            figureTransform.position = target;
        }

        
    }
}
