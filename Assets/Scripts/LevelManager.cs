using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TotalCard
{
    public GameObject LevelObj;
    //public int LevelNo;
    public int TotalCards;
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject CardPrefab;
    [SerializeField] List<TotalCard> Levels;

    [SerializeField] List<GameObject> AllCardSpawns;
    [SerializeField] List<Sprite> GameCards;
    private void Awake()
    {
        instance = this;
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

        spawncards(levelno-1);
    }

    void spawncards(int levelno)
    {
        resetcards();

        int totalcards = Levels[levelno].TotalCards;
        Transform pos= Levels[levelno].LevelObj.transform;

        for (int i = 0; i < totalcards; i++) 
        {
            GameObject NewCard = Instantiate(CardPrefab, pos.position, Quaternion.identity);
            NewCard.transform.parent = pos;

            AllCardSpawns.Add(NewCard);

            NewCard.transform.localScale = Vector3.one;

            //NewCard.GetComponent<Image>().sprite = GameManager.Instance.GetBgSprite();
        }
    }

    public void resetcards()
    {
        if(AllCardSpawns.Count> 0)
        {
            for (int i = 0; i < AllCardSpawns.Count; i++)
            {
                AllCardSpawns[i].gameObject.SetActive(false);
            }

            AllCardSpawns.Clear();
        }
        GameCards.Clear();
        GameCards = new List<Sprite>();
    }

    int looper = 0;
   
    //
    public void preparegamesprites(int levelno)
    {
      

        int index = 0;
        int menuno = GameManager.Instance.GetMenuNo();
        int totalcard = Levels[levelno-1].TotalCards;

        for (int i = 0; i < totalcard; i++)
        {
            if (index == totalcard / 2)
            {
                index = 0;
            }

            if (menuno == 0) //fruits
            {
                GameCards.Add(GameManager.Instance.GetFruitSprites()[index]);
            }
            else if (menuno == 1)  //candy
            {
                GameCards.Add(GameManager.Instance.GetCandySprites()[index]);
            }
            else if (menuno == 2) //transport
            {

                GameCards.Add(GameManager.Instance.GetTransportSprites()[index]);
            }


            index++;

        }

        shufflecards(GameCards);

        Invoke(nameof(assigncardsprite),.1f);
    }

    void shufflecards(List<Sprite> cards)
    {
        for (int i = 0; i < cards.Count; i++) 
        {
            Sprite Temp= cards[i];
            int RandomIndex=Random.Range(i, cards.Count);
            cards[i]= cards[RandomIndex];
            cards[RandomIndex]= Temp;   
        }
    }

    void assigncardsprite()
    {
        for (int i = 0; i < AllCardSpawns.Count; i++) 
        {
            AllCardSpawns[i].GetComponent<Card>().setcardsprites(GameCards[i]);
            AllCardSpawns[i].gameObject.name = GameCards[i].name;
            AllCardSpawns[i].GetComponent<Card>().setspr();

        }
    }
}

