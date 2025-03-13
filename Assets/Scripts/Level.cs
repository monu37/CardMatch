using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    Button Btn;
    [SerializeField] int LevelNo;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] GameObject[] DeactivateStars;
    [SerializeField] Sprite DeActivateStarSprite, ActivateStarSprite;
    [SerializeField] GameObject LockObj;



    private void Awake()
    {
        Btn = GetComponent<Button>();

        LevelText.text=LevelNo.ToString();
        gameObject.name = LevelNo.ToString();
    }

    private void Start()
    {
        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() => btnclick());
    }

    void btnclick()
    {
        print("Level No: " + LevelNo);
        GameManager.Instance.gameplayonoff(true);
        LevelManager.instance.chooselevel(LevelNo);
        LevelManager.instance.preparegamesprites(LevelNo);
    }
}
