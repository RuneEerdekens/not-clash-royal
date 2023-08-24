using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthScript : MonoBehaviour
{

    public GameObject DeathEffect;

    public float maxHealth;
    [SerializeField]
    private float currentHealth;
    public Gradient HealthGradient;
    public Slider Healthbar;
    private PhotonView view;

    private void Awake()    
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            view.RPC("UpdateHealthbar", RpcTarget.AllBuffered, maxHealth, maxHealth);
            currentHealth = maxHealth;
        }
    }

    [PunRPC]
    public void TakeDamage(float Amount)
    {
        if (view.IsMine)
        {
            currentHealth -= Amount;
            view.RPC("UpdateHealthbar", RpcTarget.AllBuffered, currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject); //dont use delay here or shit breaks
            }
        }
    }

    [PunRPC]
    void UpdateHealthbar(float CurrHP, float maxHP)
    {
        Healthbar.maxValue = maxHealth;
        Healthbar.value = CurrHP;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(CurrHP / maxHP);
    }

    private void OnDestroy()
    {
        Destroy(Instantiate(DeathEffect, transform.position, Quaternion.identity), 5f);
    }
}
