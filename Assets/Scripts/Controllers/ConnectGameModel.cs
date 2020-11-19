using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectGameModel : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        GameModel.Name = "The Game Name";
        GameModel.MakeGame();
    }

    private void Update()
    {
        if (GameModel.waitingOn == 0)
        {
            StartNumPlayerUpdate();
        }
    }

    private void StartNumPlayerUpdate()
    {
        GameModel.ds.UpdateNumPlayersAtLocale();
    }

}
