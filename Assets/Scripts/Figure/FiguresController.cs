using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;

public class FiguresController : MonoBehaviour
{
    public static FiguresController instance;

    private MoveFiguresTypes moveFiguresTypes;

    public MoveFiguresTypes MoveFiguresTypes => moveFiguresTypes;

    public PlayerTypes NowActiveFigure;


    // Start is called before the first frame update
    void Awake()
    {
        NowActiveFigure = PlayerTypes.FirstPlayer;
        
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoveFiguresType(int typeNum)
    {
        moveFiguresTypes = (MoveFiguresTypes) typeNum;

        StartCoroutine(SpawnFigures());
    }
    
    public IEnumerator SpawnFigures()
    {
        GameObject[,] cells = CellController.Instance.CellsList;
        
        int countOfCellsOnRowNColumn = Mathf.RoundToInt(Mathf.Sqrt(CellController.Instance.CellsCount) / 3);
        
        
        for (int i = 0; i <= countOfCellsOnRowNColumn - 1; i++) // spawn left-bottom
        {
            for (int a = 0; a <= countOfCellsOnRowNColumn - 1; a++)
            {
                
                //spawn left-bottom
                Pooler.Instance.SpawnPoolObject("FirstPlFigure", cells[i, a].transform.position - Vector3.forward,
                    Quaternion.identity);

                //spawn right-top
                Pooler.Instance.SpawnPoolObject("SecondPlFigure",
                    cells[cells.GetLength(0) - i - 1, cells.GetLength(1) - a - 1].transform.position - Vector3.forward
                    , Quaternion.identity);
                
                
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
}
