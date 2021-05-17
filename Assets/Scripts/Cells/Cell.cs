using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Cells;
using Assets.Scripts.Figure;
using Assets.Scripts.Pool;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cell : MonoBehaviour, IPooledObj
{
    protected TypeOfCell typeOfCell = TypeOfCell.White;

    public TypeOfCell TypeOfCell => typeOfCell;

    public bool hasFigure;

    private bool isStartCell;

    public FigureTypes _typeOfFigureOnCell;

    public FigureTypes TypeOfFigureOnCell => _typeOfFigureOnCell;

    public void OnObjectSpawn()
    {
        _typeOfFigureOnCell = FigureTypes.ZeroFigure;
        hasFigure = false;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.forward);

        hasFigure = hit.transform.GetComponent<Figure>();

        if (hasFigure)
        {
            _typeOfFigureOnCell = hit.transform.GetComponent<Figure>().FigureType;
        }
        else
        {
            _typeOfFigureOnCell = FigureTypes.ZeroFigure;
        }
    }
    
    public void SetColor(TypeOfCell color)
    {
        typeOfCell = color;
        
        switch (color)
        {
            case TypeOfCell.Green:
                transform.GetChild(1).gameObject.SetActive(true);
            break;
            
            case TypeOfCell.White:
                transform.GetChild(0).gameObject.SetActive(true);
            break;
        }
    }


}
