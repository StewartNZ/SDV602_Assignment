using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameModel : MonoBehaviour
{
    public static GameObject MainCanvas;
    public static GameObject HelpCommands;
    public static GameObject MapCanvas;

    public GameObject _mainCanvas;
    public GameObject _helpCommands;
    public GameObject _mapCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas = _mainCanvas;
        HelpCommands = _helpCommands;
        MapCanvas = _mapCanvas;

        GameModel.Name = "The Game Name";
        GameModel.MakeGame();
    }

}
