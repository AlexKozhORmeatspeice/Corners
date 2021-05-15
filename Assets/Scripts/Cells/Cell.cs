using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Cells;
using Assets.Scripts.Pool;
using UnityEngine;

public class Cell : MonoBehaviour, IPooledObj
{
    public TypeOfCell typeOfCell = TypeOfCell.White;
    
    public void OnObjectSpawn()
    {
        //nothing
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
