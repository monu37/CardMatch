using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public static Win instance;
    [SerializeField] GameObject[] ActivateStars;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < ActivateStars.Length; i++)
        {
            ActivateStars[i].SetActive(false);
        }
    }

    public void activatestars(int totalstar)
    {
        for (int i = 0; i < totalstar; i++)
        {
            ActivateStars[i].SetActive(true);
        }
    }
}
