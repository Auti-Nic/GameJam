using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    public List<PointInTime> pointsInTime = new List<PointInTime>();

    private Rigidbody rb;
    [SerializeField] private float maxTimeReversal;

    private bool recentlySpawned = true;

    public float TimeAlive { get; set; } = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
            transform.position = pointsInTime[0].position;
            transform.rotation = pointsInTime[0].rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
            
            if (recentlySpawned)
                Destroy(gameObject);
        }
        
        TimeAlive -= Time.fixedDeltaTime;
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(maxTimeReversal / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);

            if (recentlySpawned)
                recentlySpawned = false;
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

        TimeAlive += Time.fixedDeltaTime;
    }

    private void StopRewind()
    {
        isRewinding = false;
        if (rb)
            rb.isKinematic = false;
    }

    private void StartRewind()
    {
        isRewinding = true;
        if (rb)
            rb.isKinematic = true;
    }
}
