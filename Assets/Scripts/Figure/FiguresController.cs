using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Figure;
using UnityEngine;

public class FiguresController : MonoBehaviour
{
    public static FiguresController instance;

    private MoveFiguresTypes moveFiguresTypes;

    public MoveFiguresTypes MoveFiguresTypes => moveFiguresTypes;

    public FigureTypes NowActiveFigure;


    // Start is called before the first frame update
    void Awake()
    {
        NowActiveFigure = FigureTypes.FirstPlayer;
        
        if (instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
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

                
                CellController.Instance.startCellsFirstPl.Add(cells[i, a]);
                
                //spawn right-top
                Pooler.Instance.SpawnPoolObject("SecondPlFigure",
                    cells[cells.GetLength(0) - i - 1, cells.GetLength(1) - a - 1].transform.position - Vector3.forward
                    , Quaternion.identity);
                
                CellController.Instance.startCellsSecondPl.Add(cells[cells.GetLength(0) - i - 1, cells.GetLength(1) - a - 1]);

                
                //color start cells
                SpriteRenderer[] spriteRenderersFir = cells[i, a].GetComponentsInChildren<SpriteRenderer>();
                
                SpriteRenderer[] spriteRenderersSec = cells[cells.GetLength(0) - i - 1, cells.GetLength(1) - a - 1].GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in spriteRenderersFir)
                {
                    spriteRenderer.color = Color.yellow;
                }
                foreach (var spriteRenderer in spriteRenderersSec)
                {
                    spriteRenderer.color = Color.yellow;
                }



                
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
}
