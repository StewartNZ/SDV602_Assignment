using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System;

[Serializable]
public class Player 
{

    private string _name;
    private string _password;
    private int _health;

    [PrimaryKey]
    public string Name { get => _name; set => _name = value; }
    public string Password { get => _password; set => _password = value; }
    public int LocationID { get; set; }
    public int Health { get => _health; set => _health = value; }

    public void Move(int locationId)
    {
        LocationID = locationId;
        //GameModel.ds.SavePlayer(this);
        GameModel.ds.UpdateNumPlayersAtLocale();
        GameModel.ds.StorePlayerToJsn(this);
    }

    public static Player New(string name, string password)
    {
        Player player = new Player
        {
            Name = name,
            LocationID = GameModel.ds.GetFirstLocation().Id,
            Health = 100,
            Password = password
        };
        //GameModel.ds.SavePlayer(player);
        GameModel.ds.StorePlayerToJsn(player);
        return player;
    }
}
