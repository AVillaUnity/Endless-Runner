using UnityEngine;

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
