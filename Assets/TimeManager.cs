using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnStartRewind;
    public Action OnStopRewind;
    
    [NonSerialized] public bool isRewinding = false;
    
    public float maxTimeReversal;

    public List<TimeBody> timeBodiesDisabled = new List<TimeBody>();

    private List<ManagerPointInTime> pointsInTime = new List<ManagerPointInTime>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();

        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }
    
    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            // Enable all disabled game objects
            foreach (var timeBody in pointsInTime[0].timeBodiesDisabled)
            {
                timeBody.gameObject.SetActive(true);
            }
            
            pointsInTime.RemoveAt(0);
        }
    }

    private void Record()
    {
        // Delete points in time older than the maxTimeReversal
        if (pointsInTime.Count > Mathf.Round(maxTimeReversal / Time.fixedDeltaTime))
        {
            var pointInTime = pointsInTime[pointsInTime.Count - 1];

            // Disabled game objects can be deleted
            foreach (var timeBody in pointInTime.timeBodiesDisabled)
            {
                Destroy(timeBody.gameObject);
            }

            pointsInTime.Remove(pointInTime);
        }
        
        pointsInTime.Insert(0, new ManagerPointInTime(new List<TimeBody>(timeBodiesDisabled)));

        timeBodiesDisabled.Clear();
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
