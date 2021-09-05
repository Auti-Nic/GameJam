using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    private Rigidbody rb;

    private bool recentlySpawned = true;

    private TimeManager timeManager;

    public float TimeAlive { get; set; } = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        timeManager = FindObjectOfType<TimeManager>();

        if (!timeManager)
        {
            Debug.LogWarning($"Could not find {nameof(TimeManager)}. Disabling {nameof(TimeBody)}.");
            enabled = false;
        }
        else
        {
            timeManager.OnStartRewind += OnStartRewind;
            timeManager.OnStopRewind += OnStopRewind;
        }
    }

    private void OnStartRewind()
    {
        if (rb)
            rb.isKinematic = true;
    }
    
    private void OnStopRewind()
    {
        if (rb)
            rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (timeManager.isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            transform.position = pointsInTime[0].position;
            transform.rotation = pointsInTime[0].rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            if (recentlySpawned)
                Destroy(gameObject);
        }
        
        TimeAlive -= Time.fixedDeltaTime;
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(timeManager.maxTimeReversal / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);

            if (recentlySpawned)
                recentlySpawned = false;
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, false));

        TimeAlive += Time.fixedDeltaTime;
    }
}
