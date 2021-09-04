using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] private float lifetime;

    private float timeAlive = 0;

    private TimeBody timeBody;

    private void Start()
    {
        timeBody = GetComponent<TimeBody>();
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (timeBody.TimeAlive > lifetime)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collisioninfo) 
    {
        if (collisioninfo.collider.tag == "Player")
            Destroy(gameObject);
    }
    
}
