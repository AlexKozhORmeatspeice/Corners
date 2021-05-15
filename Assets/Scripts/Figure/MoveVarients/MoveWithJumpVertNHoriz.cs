using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;

public class MoveWithJumpVertNHoriz : IMovableFigure
{
    private Transform figureTransform;
    private float speed;
    
    
    public MoveWithJumpVertNHoriz(Transform figureTransform, float speed)
    {
        this.figureTransform = figureTransform;
        this.speed = speed;
    }
    
    public void Move(Vector3 target)
    {
        /*Vector3 direction = target - figureTransform.position;
        
        
        
        figureTransform.position = Vector3.Lerp(figureTransform.position, target - figureTransform.position,
                                            speed * Time.deltaTime);*/
    }
}
