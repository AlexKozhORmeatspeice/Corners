using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool activeCheckFigureStatus;
    
    private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        activeCheckFigureStatus = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeCheckFigureStatus)
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit)
            {
                Figure figure = hit.transform.gameObject.GetComponent<Figure>();
                
                if (figure)
                {
                    if (figure.FigureType == FiguresController.instance.NowActiveFigure)
                    {
                        activeCheckFigureStatus = false;
                        figure.SetActiveStatus(true);
                    }
                }
            }
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
