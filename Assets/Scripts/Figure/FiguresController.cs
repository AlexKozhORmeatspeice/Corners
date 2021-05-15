using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresController : MonoBehaviour
{
    public static FiguresController instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //SpawnFigures calls after work of CreatField in CellController
    public IEnumerator SpawnFigures()
    {
        GameObject[,] cells = CellController.Instance.CellsList;
        
        int countOfCellsOnRowNColumn = Mathf.RoundToInt(Mathf.Sqrt(CellController.Instance.CellsCount) / 3);
        
        for (int i = 0; i <= countOfCellsOnRowNColumn - 1; i++) // spawn left-bottom
        {
            for (int a = 0; a <= countOfCellsOnRowNColumn - 1; a++)
            {
                //spawn left-bottom
                Pooler.Instance.SpawnPoolObject("Figure", cells[i, a].transform.position - Vector3.forward, Quaternion.identity);
                //spawn right-top
                Pooler.Instance.SpawnPoolObject("Figure", cells[cells.GetLength(0) - i - 1, cells.GetLength(1) - a - 1].transform.position - Vector3.forward,
                    Quaternion.identity);
                
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
}
