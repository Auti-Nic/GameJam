using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnStartRewind;
    public Action OnStopRewind;
    
    [NonSerialized] public bool isRewinding = false;
    
    public float maxTimeReversal;

    private List<TimeBody> timeBodiesEnabled = new List<TimeBody>(); 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();

        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }
    
    private void StartRewind()
    {
        isRewinding = true;
        OnStartRewind.Invoke();
    }
    
    private void StopRewind()
    {
        isRewinding = false;
        OnStopRewind.Invoke();
    }
}
