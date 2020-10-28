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

    public static string Name { get => _name; set => _name = value; }

    public static Player currentPlayer;
    public static Location currentLocale;

    public static DataService ds = new DataService("AssignmentDatabase.db");


    public static void MakeGame()
    {
        ViewController.ShowView(ViewController.GameView.Login);
        ds.CreateTables();

        if (!ds.HaveLocations())
        {
            Location forest = Location.New("Forest", "Run!!");
            Location castle = Location.New("Castle", "Crocodiles");
            Location swamp = Location.New("Swamp", "Slimy");

            forest.AddConnection(castle, "North");
            castle.AddConnection(forest, "South");

            forest.AddConnection(swamp, "South");
            swamp.AddConnection(forest, "North");
            
        }

        //if (ds.HavePlayers())
        //{
        //    currentPlayer = ds.GetPlayer(1);
        //    currentLocale = ds.GetLocation(currentPlayer.LocationID);
        //} 
        //else 
        //{
        //    currentPlayer = Player.New("PlayerOne", "Password");

        //    currentLocale = ds.GetLocation(currentPlayer.LocationID);
        //}
    }

    public static void ChangePlayer(Player player)
    {
        currentPlayer = player;
        ds.SavePlayer(currentPlayer);
        currentLocale = ds.GetLocation(currentPlayer.LocationID);
    }

    public static bool TryMove(string direction)
    {
        Location nextLocale = currentLocale.GetConnection(direction);
        if (nextLocale == null)
        {
            return false;
        } else
        {
            currentPlayer.Move(nextLocale.Id);
            currentLocale = nextLocale;
            return true;
        }
    }

}

