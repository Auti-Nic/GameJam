using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TimeHealth : MonoBehaviour
{
    [SerializeField] private float maxTimeHealth;
    [SerializeField] private float timeHealthDepletionSpeed;
    [SerializeField] private float maxShield;
    [SerializeField] private float shieldRegenSpeed;

    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private Text shieldText;

    [SerializeField] private AudioClip shieldWarning;
    [SerializeField] private AudioClip hit;

    private AudioSource audioSource;
    
    private float health;
    private float shield;
    
    public float Shield
    {
        get => shield;
        set
        {
            shield = value;
            
            if (shield > maxShield)
                shield = maxShield;

            shieldText.text = $"Shield: {shield}";
        }
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;

            if (health > maxTimeHealth)
                health = maxTimeHealth;

            if (health <= 0)
                FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Start()
    {
        Health = maxTimeHealth;
        Shield = maxShield;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Health -= timeHealthDepletionSpeed * Time.deltaTime;

        scoreScript.scoreValue = Health;
        
        Shield += shieldRegenSpeed * Time.deltaTime;
    }
    
    public void TakeDamage(float damage)
    {
        if (Shield > 0)
        {
            if (Shield > damage)
            {
                //can defense all the damage from by shield
                Shield -= damage;
            }
            else if (Shield < damage)
            {
                Shield = 0;
                Health -= damage - Shield;

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = shieldWarning;
                    audioSource.Play();
                }
            }
            
            if (!audioSource.isPlaying)
            {
                audioSource.clip = hit;
                audioSource.Play();
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_bullet"))
        {
            TakeDamage(other.GetComponent<Bullet>().damage);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("enemy"))
            TakeDamage(1);
    }
}
