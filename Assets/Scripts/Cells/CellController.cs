using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Cells;
using Assets.Scripts.Figure;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public static CellController Instance;
    [SerializeField]
    private int cellsCount = 81; //cellsCount num need to be representable in formula x^2 

    public int CellsCount => cellsCount;

    private GameObject[,] cellsList;

    public GameObject[,] CellsList => cellsList;

    private float distBetweenCells;

    public float DistBetweenCells => distBetweenCells;

    public List<GameObject> startCellsFirstPl;
    public List<GameObject> startCellsSecondPl;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        int rowAndColumnValue = (int)Mathf.Sqrt(cellsCount); 
        cellsList = new GameObject[rowAndColumnValue,rowAndColumnValue];
        

        //need only to take a distance
        GameObject cell = Pooler.Instance.SpawnPoolObject("Cell", Vector3.zero, Quaternion.identity);
        
        distBetweenCells = cell.GetComponentInChildren<SpriteRenderer>().size.x;
        
        cell.SetActive(false);

        CreatField(cellsCount);

        StartCoroutine(CheckStartCells());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatField(int numOfCells)
    {
        //starting point selection relative to main cam 
        Vector3 startPos = Camera.main.transform.position;
        
        startPos.y -= Mathf.Floor( Mathf.Sqrt(numOfCells) / 2 * distBetweenCells);
        startPos.x -= Mathf.Floor( Mathf.Sqrt(numOfCells) / 2 * distBetweenCells);
        startPos.z = 0;
        
        Vector3 nowSpawnPos = startPos;

        for (int i = 0; i <= (int) Mathf.Sqrt(numOfCells) - 1; i++)
        {
            for (int a = 0; a <= (int) Mathf.Sqrt(numOfCells) - 1; a++)
            {
                GameObject cell = Pooler.Instance.SpawnPoolObject("Cell", nowSpawnPos, Quaternion.identity);

                cellsList[i, a] = cell;
                
                cell.GetComponent<Cell>().SetColor((a + i) % 2 == 0  ? TypeOfCell.Green : TypeOfCell.White);
                
                nowSpawnPos.x += distBetweenCells;
            }
            
            nowSpawnPos.y += distBetweenCells;
            nowSpawnPos.x = startPos.x;
        }

    }

    private IEnumerator CheckStartCells()
    {
        if (startCellsFirstPl.Count != 0 && startCellsFirstPl.All(x 
            => x.GetComponent<Cell>().TypeOfFigureOnCell == FigureTypes.SecondPlayer) )
        {
            UIContoller.instance.WinGame(FigureTypes.SecondPlayer);
        }

        if (startCellsSecondPl.Count != 0 && startCellsSecondPl.All(x 
            => x.GetComponent<Cell>().TypeOfFigureOnCell == FigureTypes.FirstPlayer) )
        {
            UIContoller.instance.WinGame(FigureTypes.FirstPlayer);
        }
        
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CheckStartCells());
    }
}
