using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

    const string HIGHSCORE_KEY = "highscore";
    const string OFFSET_X = "x";
    const string OFFSET_Y = "y";
    const string OFFSET_Z = "z";


    // Highscore
    public static void SetHighscore(float value)
    {
        if (value >= 0)
            PlayerPrefs.SetFloat(HIGHSCORE_KEY, value);
        else
            Debug.LogError("Value not greater than or equal to 0");
    }

    public static float GetHighscore()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE_KEY, 0);
    }

    public static void SetOffsetZ(float value)
    {
        PlayerPrefs.SetFloat(OFFSET_Z, value);
    }

    public static float GetOffsetZ()
    {
        return PlayerPrefs.GetFloat(OFFSET_Z, 0);
    }


}
