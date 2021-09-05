using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action OnAnimationEnd;

    private void AnimationEnd()
    {
        OnAnimationEnd.Invoke();
    }
}
