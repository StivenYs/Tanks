using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
    
    
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead;

    [SerializeField]
    bool activeShield = false;
    public GameObject Shield;
    

    [HideInInspector]

    public bool MoreDamege;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
         

        MoreDamege = false;
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    private void Update()
    {
        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        if (activeShield == false)
        {
            m_CurrentHealth -= amount;

            SetHealthUI();
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }
        }
       
    }


    private void SetHealthUI()
    {
        
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;

        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prop"))
        {
            Shield.SetActive(true);
            activeShield = true;
            StartCoroutine(SheldTime(10F));
            
            
        }
        if (other.gameObject.CompareTag("PropLife"))
        {
            m_CurrentHealth += 20;
        }
        if (other.gameObject.CompareTag("PropDama"))
        {
            MoreDamege = true;
           
        }
    }

   IEnumerator SheldTime(float Time)
    {
        yield return new WaitForSecondsRealtime(Time);
        Shield.SetActive(false);
        activeShield = false;

    }
    



}