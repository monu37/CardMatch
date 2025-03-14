using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menuoptions : MonoBehaviour
{
    [SerializeField] int MenuNo;
    Button Btn;

    private void Awake()
    {
        Btn = GetComponent<Button>();
    }

    private void Start()
    {
        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() => btnclick());
    }

    public void btnclick()
    {
        print(EventSystem.current.currentSelectedGameObject.name);
        GameManager.instance.levelonoff(true);
        GameManager.instance.setbgsprites(MenuNo);
        GameManager.instance.setmenuno(MenuNo);
    }

}
