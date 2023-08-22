using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{


    public GameObject DeathEffect;

    public float maxHealth;
    [SerializeField]
    private float currentHealth;
    public Gradient HealthGradient;
    public Slider Healthbar;

    private void Awake()    
    {
        Healthbar.maxValue = maxHealth;
        Healthbar.value = Healthbar.maxValue;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(1);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float Amount)
    {
        currentHealth -= Amount;
        Healthbar.value = currentHealth;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(currentHealth / maxHealth);


        if(currentHealth <= 0)
        {
            Destroy(Instantiate(DeathEffect, transform.position, Quaternion.identity), 5f); //
            Destroy(gameObject); //dont use delay here or shit breaks
        }
    }
}
