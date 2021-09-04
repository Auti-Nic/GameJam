using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] private float lifetime;

    private float timeAlive = 0;
    
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        
        if (timeAlive < lifetime)
            timeAlive += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collisioninfo) 
    {
        if (collisioninfo.collider.tag == "Player")
            Destroy(gameObject);
    }
    
}
