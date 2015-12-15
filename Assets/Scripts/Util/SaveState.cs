using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveState
{
	
	private const string CURRENTLEVEL = "CurrentLevel";
    private const string CURRENTWORLD = "CurrentWorld";
    private const string STARWORLDLEVELS = "Stars";
    public const int UnlockLevel0 = -1;
    public const int UnlockLevel1 = 30;
    public const int UnlockLevel2 = 60;
    public const int UnlockLevel3 = 100;
    public const int UnlockLevel4 = 140;
	
	public SaveState()
	{


	}
    public void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
   
	public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(CURRENTLEVEL, 1);

    }
	 public void SetCurrentLevel(int Level)
    {
        PlayerPrefs.SetInt(CURRENTLEVEL, Level);
        PlayerPrefs.Save();
    }
    public int GetLevelStarsBy(int Level)
    {
       return PlayerPrefs.GetInt(STARWORLDLEVELS + Level, 0);
        
    }
    public int GetAllStars()
    {
       int StarCount = 0;

        for (int i = 0; i < 56; i++)
        {
            StarCount += PlayerPrefs.GetInt(STARWORLDLEVELS + i, 0);
        }
        return StarCount;
    }
    public void OnLevelComplete(int Level, int Stars)
    {
        int StarsCount = PlayerPrefs.GetInt(STARWORLDLEVELS + Level, 0);
        if (Stars >= StarsCount)
        {
            PlayerPrefs.SetInt(STARWORLDLEVELS + Level, Stars);

            PlayerPrefs.Save();
        }
        if (Level >= GetCurrentLevel())
        {
            if (Stars > 0)
            {
                SetCurrentLevel(Level + 1);
            }
        }
    }
}
