using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectGameModel : MonoBehaviour
{
    public static GameObject MainCanvas;
    public static GameObject HelpCommandsCanvas;
    public static GameObject MapCanvas;
    public static GameObject InventoryCanvas;
    public static Text HelpCommands;

    public GameObject _mainCanvas;
    public GameObject _helpCommandsCanvas;
    public GameObject _mapCanvas;
    public GameObject _inventoryCanvas;
    public Text _helpCommands;
    
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas = _mainCanvas;
        HelpCommandsCanvas = _helpCommandsCanvas;
        MapCanvas = _mapCanvas;
        InventoryCanvas = _inventoryCanvas;

        HelpCommands = _helpCommands;

        GameModel.Name = "The Game Name";
        GameModel.MakeGame();
    }

}
