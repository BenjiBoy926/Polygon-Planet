using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * CLASS DataManager
 * -----------------
 * Contains methods for saving and loading data 
 * on the local machine
 * -----------------
 */ 

public static class DataManager 
{
	// Path on the machine the data will be saved to.  Uses a local Unity property
	// plus a small string for identification
	private static string dataPath = Application.persistentDataPath + "/haveMercy.dat";
	private static int totalLevels = 26;	// Total levels in the game
    private static int totalLores = 4;  // Total pieces of lore given throughout playing the game
	private static bool dataLoaded = false;	// True if data has been loaded
	private static PlayerData data;	// Contains data for this play through

	public static int TotalLevels { get { return totalLevels; } }
	public static bool DataLoaded { get { return dataLoaded; } }
	public static PlayerData Data { get { return data; } }

	// Save player data to the location specified
	public static void Save ()
	{
		// Set up binary formatter and open file stream
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file;	// The file the data will be loaded into

		// Load the data into the path specified
		file = File.Open (dataPath, FileMode.OpenOrCreate);
		formatter.Serialize (file, data);
		file.Close ();

		Debug.Log ("DataManager loaded the current version of PlayerData into the file");
	}

	// Load player data from the location specified,
	// or construct data from scratch if no data exists there yet
	public static void Load ()
	{
		// If a file already exists at the path,
		// load the data there into local variable
		if (File.Exists (dataPath)) {
			// Set up binary formatter, and open file at the specified path
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream file = File.Open (dataPath, FileMode.Open);

			// Load the data from that file into variable and close file
			data = (PlayerData)formatter.Deserialize (file);
			file.Close ();

			Debug.Log ("DataManager loaded an existing instance of PlayerData");
		}
		// Otherwise, construct data from scratch
		else {
			data = new PlayerData (totalLevels, totalLores);
			Debug.Log ("DataManager constructed a new instance of PlayerData");
		}

		// Set data loaded to true
		dataLoaded = true;
	}

    // Delete the file at the directory of the data
    public static void Delete ()
    {
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
    }

	// Mark given level complete and save the data
	public static void CompleteLevel (int level)
	{
		data.CompleteLevel (level);
		Save ();
	}
	// Mark given action as complete for the first time and save the data
	public static void MarkLoreAsViewed (int lore)
	{
		data.MarkLoreAsViewed (lore);
		Save ();
	}
}
