using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVED_GAME = "savedGame";

    // Saving Data based on string
    public static void SaveGame(SaveData data)
    {
        PlayerPrefs.SetString(SAVED_GAME, JsonUtility.ToJson(data));    
    }

    // Loading Data from a string
    public static SaveData LoadGame()
    {
        return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVED_GAME));
    }

    // Check if data exists
    public static bool IsGameSaved()
    {
        return PlayerPrefs.HasKey(SAVED_GAME);
    }

}
