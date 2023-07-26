using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{


    public GameObject DeathEffect;

    public float maxHealth;
    [SerializeField]
    private float currentHealth;
    public int Team;

    public Gradient HealthGradient;
    public Slider Healthbar;
    [HideInInspector]
    public LayerMask OtherTeamLayer;
    [HideInInspector]
    public int OtherTeamInt;

    private void Awake()    
    {
        Healthbar.maxValue = maxHealth;
        Healthbar.value = Healthbar.maxValue;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(1);
        currentHealth = maxHealth;
        if (gameObject.layer == 6) // layer 6 is team 1
        {
            Team = 1;
            OtherTeamLayer = LayerMask.GetMask("Team2");
            OtherTeamInt = 7;
        }
        else if (gameObject.layer == 7) // 7 is team 2
        {
            Team = 2;
            OtherTeamLayer = LayerMask.GetMask("Team1");
            OtherTeamInt = 6;
        }
    }

    public void TakeDamage(float Amount)
    {
        currentHealth -= Amount;
        Healthbar.value = currentHealth;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(currentHealth / maxHealth);
        if(currentHealth <= 0)
        {
            Destroy(Instantiate(DeathEffect, transform.position, Quaternion.identity), 5f);
            Destroy(gameObject); //dont use delay here or shit breaks
        }
    }
}
