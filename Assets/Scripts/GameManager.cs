using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject MenuPanel, LevelPanel, GamePlayPanel;
    [SerializeField] List<Sprite> BgSprites;
    [SerializeField] List<Sprite> FruitsSprites;
    [SerializeField] List<Sprite> CandySprites;
    [SerializeField] List<Sprite> TransportSprites;

    [SerializeField] Sprite BgCoverSprite;
    [SerializeField] int MenuNo;

    private void Awake()
    {
        Instance = this;

        mainmenuonoff(true);
    }

    public void mainmenuonoff(bool b)
    {
        MenuPanel.SetActive(b);
        LevelPanel.SetActive(!b);
        GamePlayPanel.SetActive(!b);

    }
    public void levelonoff(bool b)
    {
        MenuPanel.SetActive(!b);
        LevelPanel.SetActive(b);
        GamePlayPanel.SetActive(!b);

        LevelManager.instance.resetcards();

    }
    public void gameplayonoff(bool b)
    {
        MenuPanel.SetActive(!b);
        LevelPanel.SetActive(!b);
        GamePlayPanel.SetActive(b);

    }

    //

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
}
