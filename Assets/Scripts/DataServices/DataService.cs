using SQLite4Unity3d;
using UnityEngine;
using System.Linq;
// DataService is a bridge to SQlite 
// =================================
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService {

    private SQLiteConnection _connection;

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


    public void CreateTables()
    {
        //_connection.DropTable<Location>();
        //_connection.DropTable<LocationConnection>();
        //_connection.DropTable<Player>();

        _connection.CreateTable<Location>();
        _connection.CreateTable<LocationConnection>();
        _connection.CreateTable<Player>();
    }

    public Location SaveLocation(Location location)
    {
        _connection.Insert(location);
        return location;
    }

    public void SaveLocationConnection(LocationConnection connection)
    {
        _connection.Insert(connection);
    }

    public Location GetLocation(int id)
    {
        return _connection.Table<Location>().Where(l => l.Id == id).FirstOrDefault();
    }

    public Location GetFirstLocation()
    {
        return _connection.Table<Location>().FirstOrDefault();
    }

    public Location GetConnection(int currentId, string direction)
    {
        LocationConnection connection = _connection.Table<LocationConnection>().Where(lc => lc.FromId == currentId && lc.Direction == direction).FirstOrDefault();
        return GetLocation(connection.ToId);
    }

    public bool HaveLocations ()
    {
        return _connection.Table<Location>().Count() > 0;
    }

    public Player SavePlayer(Player player)
    {
        Player receive = _connection.Table<Player>().Where(l => l.Id == player.Id).FirstOrDefault();
        if (receive == null)
        {
            _connection.Insert(player);
        } else
        {
            _connection.Update(player);
        }
        return player;
    }

    public Player GetPlayer(int id)
    {
        return _connection.Table<Player>().Where(l => l.Id == id).FirstOrDefault();
    }

    public Player GetPlayer(string username)
    {
        return _connection.Table<Player>().Where(l => l.Name == username).FirstOrDefault();
    }

    public bool HavePlayers()
    {
        return _connection.Table<Player>().Count() > 0;
    }
}
