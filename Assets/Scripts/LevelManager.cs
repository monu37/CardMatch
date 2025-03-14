using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TotalCard
{
    public GameObject LevelObj;
    public int TotalCards;
    //public int TotalWinTurn;
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] List<TotalCard> Levels;

   
    private void Awake()
    {
        instance = this;
    }

    public List<TotalCard> GetLevels()
    {
        return Levels;
    }
    public void chooselevel(int levelno)
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if (i == levelno - 1)
            {
                Levels[levelno - 1].LevelObj.SetActive(true);
            }
            else
            {
                Levels[i].LevelObj.SetActive(false);
            }
        }

       GameManager.instance.spawncards(levelno-1);
    }

   
   

}

