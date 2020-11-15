using SQLite4Unity3d;
using UnityEngine;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System;
// DataService is a bridge to SQlite 
// =================================
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService { 

    private SQLiteConnection _connection;
    public SQLiteConnection Connection { get => _connection; }

    JSONDropService jsnDrop = new JSONDropService { Token = "499a8449-6e83-447b-9678-8e41f6161a5c" };

    public DataService(string DatabaseName) {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

    }

    private void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ... " + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }

    private void jsnReceivePlayers(List<Player> recievedPlayers)
    {
        foreach (Player player in recievedPlayers)
        {
            SavePlayer(player);
        }
    }

    public void CreateTables()
    {
        //_connection.DropTable<Location>();
        //_connection.DropTable<LocationConnection>();
        _connection.DropTable<Player>();

        //jsnDrop.Drop<Player>(jsnReceiverDel);

        Connection.CreateTable<Location>();
        Connection.CreateTable<LocationConnection>();
        Connection.CreateTable<Player>();

        //jsnDrop.Create<Player>(new Player
        //{
        //    Name = "UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU",
        //    Password = "**************************************************",
        //    LocationID = 0,
        //    Health = 0
        //}, jsnReceiverDel);

        jsnDrop.All<Player, JsnReceiver>(jsnReceivePlayers, jsnReceiverDel);
    }

    public Location SaveLocation(Location location)
    {
        Connection.Insert(location);
        return location;
    }

    public void SaveLocationConnection(LocationConnection connection)
    {
        Connection.Insert(connection);
    }

    public Location GetLocation(int id)
    {
        return Connection.Table<Location>().Where(l => l.Id == id).FirstOrDefault();
    }

    public Location GetFirstLocation()
    {
        return Connection.Table<Location>().FirstOrDefault();
    }

    public Location GetConnection(int currentId, string direction)
    {
        LocationConnection connection = Connection.Table<LocationConnection>().Where(lc => lc.FromId == currentId && lc.Direction == direction).FirstOrDefault();
        if (connection == null)
        {
            return null;
        }
        else
        {
            return GetLocation(connection.ToId);
        }
    }

    public bool HaveLocations ()
    {
        return Connection.Table<Location>().Count() > 0;
    }

    public Player SavePlayer(Player player)
    {
        Player receive = Connection.Table<Player>().Where(p => p.Name == player.Name).FirstOrDefault();
        if (receive == null)
        {
            Connection.Insert(player);
        } else
        {
            Connection.Update(player);
        }
        return player;
    }

    public void StorePlayerToJsn(Player player)
    {
        jsnDrop.Store<Player>(player, jsnReceiverDel);
    }

    public Player GetPlayer(string username)
    {
        return Connection.Table<Player>().Where(l => l.Name == username).FirstOrDefault();
    }

    public bool HavePlayers()
    {
        return Connection.Table<Player>().Count() > 0;
    }
}
