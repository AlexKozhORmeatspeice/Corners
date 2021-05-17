using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        target.z -= 1.0f;

        Vector3 direction = target - figureTransform.position;
        direction.z = 0;

        float distance = Vector2.Distance((Vector2) figureTransform.transform.position, target);
        
        if (distance >= distToMove)
        { 
            //vert move
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)figureTransform.position, direction, distToMove);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform != figureTransform && hit.transform.GetComponent<Figure>())
                {
                    if (direction.x != 0 && direction.y != 0 && distance <= distToMove * 2)
                    {
                        figureTransform.transform.position = target;
                    }
                    break;
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
