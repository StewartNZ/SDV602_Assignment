using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameModel : MonoBehaviour
{
    public static GameObject mainGameView;
    public static GameObject inventoryView;
    public static GameObject mapView;

    public GameObject _mainGameView;
    public GameObject _inventoryView;
    public GameObject _mapView;
    
    // Start is called before the first frame update
    void Start()
    {
        mainGameView = _mainGameView;
        inventoryView = _inventoryView;
        mapView = _mapView;

        GameModel.Name = "The Game Name";
        GameModel.MakeGame();
    }

}
