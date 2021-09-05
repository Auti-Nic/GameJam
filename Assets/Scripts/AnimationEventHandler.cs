using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action OnAnimationEnd;
    public Action<string> OnAnimationEvent;

    private void AnimationEnd()
    {
        OnAnimationEnd.Invoke();
    }

    private void AnimationEvent(string eventName)
    {
        OnAnimationEvent.Invoke(eventName);
    }
}
