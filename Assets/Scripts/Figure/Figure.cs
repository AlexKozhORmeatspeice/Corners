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

    [SerializeField]
    private FigureTypes figureType;

    public FigureTypes FigureType => figureType;


    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        _startColor = GetComponentInChildren<SpriteRenderer>().color;
        mainCamera = Camera.main;
        active = false;

        switch (FiguresController.instance.MoveFiguresTypes)
        {
            case MoveFiguresTypes.VertMove:
                SetWalkBehavior(new MoveWithJumpVert(this.transform, speed));
                break;
            
            case MoveFiguresTypes.OneCellMove:
                SetWalkBehavior(new MoveOnOneCell(this.transform, speed));
                break;
            
            case MoveFiguresTypes.HorAndVertMove:
                SetWalkBehavior(new MoveWithJumpVertNHoriz(this.transform, speed));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit)
            { 
                //if pressed another playable figure set it active
                Figure figure = hit.transform.gameObject.GetComponent<Figure>();
                if (figure && figure.FigureType == FiguresController.instance.NowActiveFigure)
                {
                    this.SetActiveStatus(false);
                    figure.SetActiveStatus(true);
                }
                
                
                //move on a selected cell
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();

                if (cell && !cell.hasFigure)
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
        
        //check changed position figure or not
        if ((Vector2)transform.position == (Vector2)pos)
        {
            if (FigureType == FigureTypes.FirstPlayer)
            {
                FiguresController.instance.NowActiveFigure = FigureTypes.SecondPlayer;
            }
            else
            {
                FiguresController.instance.NowActiveFigure = FigureTypes.FirstPlayer;
            }
        }
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
