using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using Assets.Scripts.Pool;
using UnityEngine;

public class Figure : MonoBehaviour, IPooledObj
{
    [SerializeField]
    private float speed = 3.0f;

    private bool active;
    
    private IMovableFigure moveBehavior;

    private Camera mainCamera;

    private Color _startColor;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        _startColor = GetComponentInChildren<SpriteRenderer>().color;
        mainCamera = Camera.main;
        active = false;
        
        moveBehavior = new MoveWithJumpVert(transform, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit)
            {
                Figure figure = hit.transform.gameObject.GetComponent<Figure>();
                if (figure)
                {
                    this.SetActiveStatus(false);
                    figure.SetActiveStatus(true);
                }
                
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();

                if (cell)
                {
                    Move(cell.gameObject.transform.position);
                }
            }

        }
    }

    private void Move(Vector3 pos)
    {
        SetActiveStatus(false);
        PlayerController.instance.activeCheckFigureStatus = true;

        moveBehavior.Move(pos);
    }

    public void SetWalkBehavior(IMovableFigure movableFigure)
    {
        moveBehavior = movableFigure;
    }

    public void SetActiveStatus(bool active)
    {
        this.active = active;
        if (active == false)
        {
            GetComponentInChildren<SpriteRenderer>().color = _startColor;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
        
    }
    
}
