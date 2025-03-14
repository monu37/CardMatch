using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public static Card Instance;

    [SerializeField] int CardIndex;
    Button btn;
    Animator Anim;
    [SerializeField] Sprite CardSprite;
    Image img;
    private void Awake()
    {
        Instance = this;
        img = GetComponent<Image>();
        Anim = GetComponent<Animator>();
        btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Invoke(nameof(backcardflip), .5f);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => cardflip());
    }

    public void setcardindex(int i)
    {
        CardIndex = i;
    }

    public void setcardsprites(Sprite _sprite)
    {
        CardSprite = _sprite;
    }

    public void setspr()
    {
        img.sprite = CardSprite;
    }

    public void cardflip()
    {
        StartCoroutine(flip());
        //GameManager.instance.setguessindex(gameObject.name);
        GameManager.instance.checkmatch(CardIndex);
    }
    public void backcardflip()
    {
        StartCoroutine(flipback());
    }

    public void fadeout()
    {
        StartCoroutine(cardfade());
    }

    IEnumerator flip()
    {
        Anim.Play("TurnUp");
        yield return new WaitForSeconds(.6f);
        img.sprite = CardSprite;
    }

    IEnumerator flipback()
    {
        Anim.Play("TurnBack");
        yield return new WaitForSeconds(.6f);
        img.sprite = GameManager.instance.GetBgSprite();
    }

    IEnumerator cardfade()
    {
        yield return new WaitForSeconds(.3f);
        Anim.Play("FadeOut");
    }


}
