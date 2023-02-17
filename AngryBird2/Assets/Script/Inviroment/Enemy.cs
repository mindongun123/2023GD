using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public float _heath;

    [Header("Audio")]
    public AudioSource auEnemy;

    private void Start()
    {
        DOVirtual.DelayedCall(Random.Range(3, 7), PlayAudio);
    }

    public void PlayAudio()
    {
        auEnemy.Play();
        DOVirtual.DelayedCall(Random.Range(3, 7), PlayAudio);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("angrybird") || other.gameObject.CompareTag("area11"))
        {
            transform.DOKill();
            Destroy(gameObject);
            GameManager.instance.data._score = GameManager.instance.data._score + 30;
            ControllerPlayGame.instance.auCoin.Play();
        }
        else
        {
            if (GameController.instance.stateGame == Enums.StateGame.Start)
            {
                float dam = gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude;

                if (gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0.2f)
                {
                    _heath = _heath - dam * 5;
                    animator.SetBool("ShootEnemy", true);
                    UpScore(dam);
                }
                else
                {
                    animator.SetBool("ShootEnemy", false);
                }
            }
        }
    }

    public void UpScore(float dam)
    {
        auEnemy.volume = GameController.instance._volumeGame;
        if (dam < 1)
        {
            GameManager.instance.data._score = GameManager.instance.data._score + (int)(dam * 4);
            ControllerPlayGame.instance.auCoin.Play();
        }
        else
        {
            GameManager.instance.data._score = GameManager.instance.data._score + (int)(dam * 2);
            ControllerPlayGame.instance.auCoin.Play();
        }
    }

    private void Update()
    {
        if (_heath <= 0)
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
