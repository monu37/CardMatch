using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Helper : MonoBehaviour
{


    //save level
    public static void setlevel(int menu,int unlocklevel)
    {
        PlayerPrefs.SetInt("Level" + menu, unlocklevel);
    }
    public static int GetLevel(int menuno) 
    {
        return PlayerPrefs.GetInt("Level" + menuno);
    }

    //save star
    public static void setstar(int menuno,int level,int star)
    {
        PlayerPrefs.SetInt("star" + menuno + level, star);

    }
    public static int GetStar(int menuno, int level) 
    {
        return PlayerPrefs.GetInt("star" + menuno + level);
    }


}

