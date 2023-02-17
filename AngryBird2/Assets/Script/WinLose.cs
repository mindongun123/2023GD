using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WinLose : MonoBehaviour
{
    [Header("Button Menu In LoseWin")]
    public GameObject Next;
    public GameObject Home;

    public AudioSource auWinLose;
    public static WinLose instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        auWinLose.Play();
        auWinLose.loop = true;
    }

    public void Show()
    {
        DOVirtual.DelayedCall(4, ShowButton);
    }

    public void ShowButton()
    {
        Next.SetActive(true);
        Home.SetActive(true);
    }


}
