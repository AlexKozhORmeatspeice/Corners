using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Figure;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIContoller : MonoBehaviour
{
    public static UIContoller instance;
    
    [SerializeField] private Text activePlayerText;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject winGameGM;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void WinGame(FigureTypes winPlayer)
    {
        winGameGM.SetActive(true);
        
        winGameGM.transform.GetChild(0).GetComponent<Text>().text = $"Win of {winPlayer}";
    }
    
    private void Update()
    {
        activePlayerText.text = $"Now Active Player: {FiguresController.instance.NowActiveFigure.ToString()} \n" +
                                $"Now Move Var is {FiguresController.instance.MoveFiguresTypes.ToString()}";
    }



}
