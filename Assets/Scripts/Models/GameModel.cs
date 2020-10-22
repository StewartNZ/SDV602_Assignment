using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;



using System.Text;
using System.Transactions;

// Is this a factory?

public static class GameModel
{

	static String _name;

	public static string Name{
		get 
		{ 
			return _name;  
		}
		set{
			_name = value; 
		}

	}

    public static Location currentLocale;
    public static Player currentPlayer;
    public static GameView currentView = GameView.MainGame;

    public enum GameView
    {
        MainGame,
        Inventory,
        Map
    }

    public static void MakeGame()
    {
        ShowView(GameView.MainGame);

        Location forest = new Location
        {
            Name = "Forest",
            Story = "Run!!"
        };

        Location castle = new Location
        {
            Name = "Castle",
            Story = "Crocodiles"
        };

        Location swamp = new Location
        {
            Name = "Swamp",
            Story = "Slimy"
        };

        currentLocale = forest;

        forest.addLocation("North", castle);
        castle.addLocation("South", forest);

        forest.addLocation("South", swamp);
        swamp.addLocation("North", forest);

    }

    public static void ShowView(GameView prGameView)
    {
        if (prGameView == GameView.MainGame)
        {
            ConnectGameModel.MainCanvas.SetActive(true);
            ConnectGameModel.MapCanvas.SetActive(false);

            currentView = GameView.MainGame;
            Debug.Log("MainView");
        }
        else if (prGameView == GameView.Inventory)
        {
            //ConnectGameModel.mainGameView.SetActive(false);
            //ConnectGameModel.inventoryView.SetActive(true);
            //ConnectGameModel.mapView.SetActive(false);

            currentView = GameView.Inventory;
            Debug.Log("MainView");
        }
        else if (prGameView == GameView.Map)
        {
            ConnectGameModel.MainCanvas.SetActive(false);
            ConnectGameModel.MapCanvas.SetActive(true);

            currentView = GameView.Map;
            Debug.Log("MainView");
        }
    }

    public static void ToggleHelp()
    {
        ConnectGameModel.HelpCommands.SetActive(ConnectGameModel.HelpCommands.activeInHierarchy ? false : true);
    }

}

