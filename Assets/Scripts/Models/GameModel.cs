using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;



using System.Text;
using System.Transactions;
using UnityEngine.UI;

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

    private static string[] mainGameHelpCommands = new string[]
    {
        "- go (north/south/east/west)",
        "- show (map/inventory)",
        "- current (story)"
    };

    private static string[] inventoryHelpCommands = new string[]
    {
        "- back"
    };

    private static string[] mapHelpCommands = new string[]
    {
        "- back"
    };

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
            ConnectGameModel.InventoryCanvas.SetActive(false);

            UpdateHelpCommands(mainGameHelpCommands);

            currentView = GameView.MainGame;
            Debug.Log("MainView");
        }
        else if (prGameView == GameView.Inventory)
        {
            ConnectGameModel.MainCanvas.SetActive(false);
            ConnectGameModel.MapCanvas.SetActive(false);
            ConnectGameModel.InventoryCanvas.SetActive(true);

            UpdateHelpCommands(inventoryHelpCommands);

            currentView = GameView.Inventory;
            Debug.Log("MainView");
        }
        else if (prGameView == GameView.Map)
        {
            ConnectGameModel.MainCanvas.SetActive(false);
            ConnectGameModel.MapCanvas.SetActive(true);
            ConnectGameModel.InventoryCanvas.SetActive(false);

            UpdateHelpCommands(mapHelpCommands);

            currentView = GameView.Map;
            Debug.Log("MainView");
        }
    }

    public static void ToggleHelp()
    {
        ConnectGameModel.HelpCommandsCanvas.SetActive(ConnectGameModel.HelpCommandsCanvas.activeInHierarchy ? false : true);
    }

    private static void UpdateHelpCommands(string[] commands)
    {
        StringBuilder helpCommands = new StringBuilder();
        foreach (string line in commands)
        {
            helpCommands.AppendLine(line);
        }

        ConnectGameModel.HelpCommands.text = helpCommands.ToString();
    }

}

