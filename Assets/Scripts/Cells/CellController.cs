using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Cells;
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


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        int rowAndColumnValue = (int)Mathf.Sqrt(cellsCount); 
        cellsList = new GameObject[rowAndColumnValue,rowAndColumnValue];
        

        //need only to take a distance
        GameObject cell = Pooler.Instance.SpawnPoolObject("Cell", Vector3.zero, Quaternion.identity);
        
        distBetweenCells = cell.GetComponentInChildren<SpriteRenderer>().size.x;
        
        cell.SetActive(false);
        
    }
    
    private void Start()
    {
        CreatField(cellsCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatField(int numOfCells)
    {
        //starting point selection relative to main cam center
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

        StartCoroutine(FiguresController.instance.SpawnFigures());
    }
}
