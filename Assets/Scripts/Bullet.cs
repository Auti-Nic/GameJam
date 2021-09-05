using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    public float damage;

    private TimeBody timeBody;
    private TimeManager timeManager;

    private void Start()
    {
        timeBody = GetComponent<TimeBody>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (!timeManager.isRewinding && timeBody.TimeAlive >= lifetime)
            gameObject.SetActive(false);
        
        if (timeManager.isRewinding && timeBody.TimeAlive < lifetime)
            gameObject.SetActive(true);
    }
}
