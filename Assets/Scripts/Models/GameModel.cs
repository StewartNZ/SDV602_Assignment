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
    public static int waitingOn = 0;
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
            Location nahs = Location.New("Nahs", "Welcome to Nahs, your homeworld. Your first mission as a captain is explore the whole system. The map might help with this. If you need help, use the help command.");

            Location hilo = Location.New("Hilo", "Hilo, the cloudy planet of your home system. These clouds are often used as a tourist atraction because of the sheer size of them and how different they look to Nahs. These clouds are also a brilliant fuel source for interplanetary travel and are hoped to lead to intersteller travel.");

            Location atronk = Location.New("Atronk", "Atronk, this world has a similar look to an atom making it a very popular tourist attraction and interest of the best scientists.");

            Location purplus = Location.New("Purplus", "Purplus has rings of clouds surrounding its unique purple surface making it an interesting planet to vist for any prospect explorer.");

            Location crasal = Location.New("Crasal", "Crasal is known as the large ice ball of the system even though it is actually made out of a unique crystal that is harder than any material found on Nahs. In fact the material is so hard that we currently don't have the technology to even take a sample of it.");

            Location ruckus = Location.New("Ruckus", "Ruckus is constantly under pull from the black hole but somehow still manages to stay in orbit without be destroyed. This phenomema has been studyed constantly since it was first found. But because it is under the constant stress of being pulled by the black hole the surface has become an extreme wasteland.");

            Location blackHole = Location.New("Black Hole", "The black hole, for its incredible size it has a remarkably low pull making semi close encounters not as dangerous as one would expect. Our ships shields can hold up to the forces for decent periods of time before needing to leave to recharge.");

            nahs.AddConnection(hilo, "North");
            nahs.AddConnection(atronk, "East");

            hilo.AddConnection(nahs, "South");
            hilo.AddConnection(atronk, "East");
            hilo.AddConnection(purplus, "North");

            atronk.AddConnection(nahs, "West");
            atronk.AddConnection(hilo, "North");
            atronk.AddConnection(crasal, "East");

            purplus.AddConnection(hilo, "South");
            purplus.AddConnection(ruckus, "East");

            crasal.AddConnection(atronk, "West");
            crasal.AddConnection(ruckus, "North");

            ruckus.AddConnection(purplus, "West");
            ruckus.AddConnection(crasal, "South");
            ruckus.AddConnection(blackHole, "East");

            blackHole.AddConnection(ruckus, "West");
            
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
        //ds.SavePlayer(currentPlayer);
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

