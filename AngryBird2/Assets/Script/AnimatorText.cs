using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimatorText : MonoBehaviour
{
    public int _timeDelayAnimation;
    public float _velocity;
    public Vector3[] path;
    private void Start()
    {
        DOVirtual.DelayedCall(_timeDelayAnimation, ViewAnimation);
    }

    public void ViewAnimation()
    {
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = transform.position - path[i];
        }
        transform.DOPath(path, _velocity).SetEase(Ease.Linear).SetLoops(-1).SetSpeedBased();
    }
}
