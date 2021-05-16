using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIContoller : MonoBehaviour
{
    [SerializeField] private Text activePlayerText;
    // Start is called before the first frame update

    private void Update()
    {
        activePlayerText.text = $"Now Active Player: {FiguresController.instance.NowActiveFigure.ToString()} \n" +
                                $"Now Move Var is {FiguresController.instance.MoveFiguresTypes.ToString()}";
    }



}
