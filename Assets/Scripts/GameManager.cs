using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject MenuPanel, LevelPanel, GamePlayPanel, WinPanel;
    [SerializeField] List<Sprite> BgSprites;
    [SerializeField] List<Sprite> FruitsSprites;
    [SerializeField] List<Sprite> CandySprites;
    [SerializeField] List<Sprite> TransportSprites;

    [SerializeField] Sprite BgCoverSprite;
    [SerializeField] int MenuNo, LevelNo;


    [SerializeField] GameObject CardPrefab;
    [SerializeField] List<GameObject> AllCardSpawns;
    [SerializeField] List<Sprite> GameCards;



    int FirstGuessIndex, SecondGuessIndex;
    bool FirstGuess, Secondguess;
    string FirstGuessName, SecondGuessName;

   [SerializeField] int TurnsCount, MatchCount;
    [SerializeField] TextMeshProUGUI MatchCountText, TurnsCountText;


    private void Awake()
    {
        instance = this;
        
        mainmenuonoff(true);
    }

   


    public void mainmenuonoff(bool b)
    {
        MenuPanel.SetActive(b);
        LevelPanel.SetActive(!b);
        GamePlayPanel.SetActive(!b);
        WinPanel.SetActive(!b);


    }
    public void levelonoff(bool b)
    {
        MenuPanel.SetActive(!b);
        LevelPanel.SetActive(b);
        GamePlayPanel.SetActive(!b);
        WinPanel.SetActive(!b);


        //LevelManager.instance.resetcards();
        resetcards();

    }
    public void gameplayonoff(bool b)
    {
        MenuPanel.SetActive(!b);
        LevelPanel.SetActive(!b);
        GamePlayPanel.SetActive(b);
        WinPanel.SetActive(!b);


    }

    public void winonoff(bool b)
    {
        WinPanel.SetActive(b);

        MenuPanel.SetActive(!b);
        LevelPanel.SetActive(!b);
        GamePlayPanel.SetActive(!b);
        //resetcards();
    }
    //

    public void setlevelno(int i)
    {
        LevelNo = i;
    }

    public Sprite GetBgSprite()
    {
        return BgCoverSprite;
    }
    public void setbgsprites(int optionno)
    {
        BgCoverSprite = BgSprites[optionno];
    }

    //
    public void setmenuno(int i)
    {
        MenuNo = i;
    }
    public int GetMenuNo()
    {
        return MenuNo;
    }

    //
    public List<Sprite> GetFruitSprites()
    {
        return FruitsSprites;
    }
    public List<Sprite> GetCandySprites()
    {
        return CandySprites;
    }
    public List<Sprite> GetTransportSprites()
    {
        return TransportSprites;
    }

    //public void setguessindex(string Guessname)
    //{
    //    if (GuessNumber >= 2)
    //    {
    //        GuessNumber = 0;
    //    }
    //    GuessNumber++;
    //    if (GuessNumber == 1)
    //    {
    //        FirstGuess = true;
    //        FirstGuessName = Guessname;
    //    }
    //    if (GuessNumber == 2)
    //    {
    //        Secondguess = true;
    //        SecondGuessName = Guessname;
    //    }

    //    if (FirstGuessName == SecondGuessName)
    //    {
    //        print("Match");

    //    }
    //    else
    //    {
    //        print("Not match yet");
    //    }

    //}

    // card spawn
    public void spawncards(int levelno)
    {
        resetcards();

        int totalcards = LevelManager.instance.GetLevels()[levelno].TotalCards;
        Transform pos = LevelManager.instance.GetLevels()[levelno].LevelObj.transform;

        for (int i = 0; i < totalcards; i++)
        {
            GameObject NewCard = Instantiate(CardPrefab, pos.position, Quaternion.identity);
            NewCard.transform.parent = pos;

            AllCardSpawns.Add(NewCard);

            NewCard.transform.localScale = Vector3.one;
            NewCard.GetComponent<Card>().setcardindex(i);

            //NewCard.GetComponent<Image>().sprite = GameManager.Instance.GetBgSprite();
        }
    }

    public void resetcards()
    {
        if (AllCardSpawns.Count > 0)
        {
            for (int i = 0; i < AllCardSpawns.Count; i++)
            {
                AllCardSpawns[i].gameObject.SetActive(false);
            }

            AllCardSpawns.Clear();
        }

        GameCards.Clear();
        GameCards = new List<Sprite>();

        TurnsCount = 0;
        MatchCount = 0;

        TurnsCountText.text = TurnsCount.ToString();
        MatchCountText.text = MatchCount.ToString();
    }

    int looper = 0;

    //
    public void preparegamesprites(int levelno)
    {
        int index = 0;
        int menuno = GameManager.instance.GetMenuNo();
        int totalcard = LevelManager.instance.GetLevels()[levelno - 1].TotalCards;

        for (int i = 0; i < totalcard; i++)
        {
            if (index == totalcard / 2)
            {
                index = 0;
            }

            if (menuno == 0) //fruits
            {
                GameCards.Add(GameManager.instance.GetFruitSprites()[index]);
            }
            else if (menuno == 1)  //candy
            {
                GameCards.Add(GameManager.instance.GetCandySprites()[index]);
            }
            else if (menuno == 2) //transport
            {

                GameCards.Add(GameManager.instance.GetTransportSprites()[index]);
            }


            index++;

        }

        shufflecards(GameCards);

        Invoke(nameof(assigncardsprite), .1f);
    }

    void shufflecards(List<Sprite> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Sprite Temp = cards[i];
            int RandomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[RandomIndex];
            cards[RandomIndex] = Temp;
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

    //
    public void checkmatch(int cardindex)
    {
        TurnsCount++;
        TurnsCountText.text = TurnsCount.ToString();

        if (!FirstGuess)
        {
            FirstGuess = true;
            FirstGuessIndex = cardindex;
            FirstGuessName = GameCards[FirstGuessIndex].name;
        }
        else if (!Secondguess)
        {
            Secondguess = true;
            SecondGuessIndex = cardindex;
            SecondGuessName = GameCards[SecondGuessIndex].name;

            //

            StartCoroutine(matchcondition());

        }


    }

    IEnumerator matchcondition()
    {
        yield return new WaitForSeconds(1.5f);

        if (FirstGuessName == SecondGuessName)
        {

            print("Matched");


            AllCardSpawns[FirstGuessIndex].GetComponent<Card>().fadeout();
            AllCardSpawns[SecondGuessIndex].GetComponent<Card>().fadeout();

            MatchCount++;
            MatchCountText.text = MatchCount.ToString();

            Invoke(nameof(win), .3f);

        }
        else /*if (FirstGuessName != SecondGuessName)*/
        {
            AllCardSpawns[FirstGuessIndex].GetComponent<Card>().backcardflip();
            AllCardSpawns[SecondGuessIndex].GetComponent<Card>().backcardflip();


        }

        yield return new WaitForSeconds(.5f);
        FirstGuess = false;
        Secondguess = false;

        FirstGuessName = " ";
        SecondGuessName = " ";
    }

    void win()
    {
        int WinCount = LevelManager.instance.GetLevels()[LevelNo-1].TotalCards / 2;
        int TotalWinCount=WinCount+2;
        if (MatchCount == WinCount)
        {
            winonoff(true);

            int totalstar = 1;
            if (TurnsCount == TotalWinCount)
            {
                totalstar = 3;
                print("3 Star");
            }
            else if (TurnsCount > TotalWinCount + 1 && TurnsCount <= TotalWinCount + 4)
            {
                totalstar = 2;
                print("2 Star");

            }
            else if (TurnsCount >= TotalWinCount + 4 )
            {
                totalstar = 1;
                print("1 Star");

            }

            Helper.setstar(MenuNo, LevelNo, totalstar);
           

            Win.instance.activatestars(totalstar);
        }


    }
}
