using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    Button Btn;
    [SerializeField] int LevelNo;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] GameObject[] DeactivateStars;
    [SerializeField] Sprite ActivateStarSprite;
    [SerializeField] GameObject LockIcon;
    int MenuNo;


    private void Awake()
    {
        Btn = GetComponent<Button>();

        LevelText.text = LevelNo.ToString();
        gameObject.name = LevelNo.ToString();
    }

    private void Start()
    {
        MenuNo=GameManager.instance.GetMenuNo();

        
        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() => btnclick());

    }
    private void OnEnable()
    {
        if (LevelNo == 1)
        {
            Helper.setlevel(MenuNo, 2);
            print("IsLevelUnlocked "+ Helper.GetLevel(MenuNo)); 
        }

        //print("Level No" + LevelNo);
        int levelcheck = Helper.GetLevel(MenuNo);
        print("IsLevel Unlocked " + levelcheck+"Level no "+LevelNo);
        int star = Helper.GetStar(MenuNo, LevelNo);

        if (levelcheck == 2)
        {
            LockIcon.SetActive(false);
            if (star > 0)
            {
                for (int i = 0; i < star; i++)
                {
                    DeactivateStars[i].GetComponent<Image>().sprite = ActivateStarSprite;
                }
            }
            Btn.enabled = true;
        }
        else
        {
            LockIcon.SetActive(true);
            Btn.enabled = false;

        }

    }

    void btnclick()
    {
        print("Level No: " + LevelNo);
        GameManager.instance.gameplayonoff(true);
        LevelManager.instance.chooselevel(LevelNo);
        GameManager.instance.preparegamesprites(LevelNo);
        GameManager.instance.setlevelno(LevelNo);
    }


}
