using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

public class Location 
{
    private string _name;
    private string _story;

    // what about location assests??

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get => _name; set => _name = value; }
    public string Story { get => _story; set => _story = value; }
    public int NumOfPlayers { get; set; }

    public static Location New(string name, string story)
    {
        Location location = new Location
        {
            Name = name,
            Story = story
        };
        location = GameModel.ds.SaveLocation(location);
        return location;
    }

    public void AddConnection(Location location, string direction)
    {
        LocationConnection connection = new LocationConnection
        {
            FromId = this.Id,
            Direction = direction,
            ToId = location.Id
        };

        GameModel.ds.SaveLocationConnection(connection);
    }

    public Location GetConnection(string direction)
    {
        return GameModel.ds.GetConnection(this.Id, direction);
    }
}

public class LocationConnection
{
    public int FromId { get; set; }
    public string Direction { get; set; }
    public int ToId { get; set; }
}
