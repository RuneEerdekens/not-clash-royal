using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public float maxMana;
    public float RegenSpeed;
    public float Cost;

    public Slider ManaSlider;
    public Slider CostManabarSlider;

    public float CurrMana;

    private void Start()
    {
        CurrMana = maxMana;
        ManaSlider.maxValue = maxMana;
        ManaSlider.value = maxMana;
        CostManabarSlider.maxValue = maxMana;
    }

    public void RemoveMana(float Amount)
    {
        CurrMana -= Amount;
    }

    private void Update()
    {
        if (CurrMana < maxMana)
        {
            CurrMana += RegenSpeed * Time.deltaTime;
            if(CurrMana > maxMana)
            {
                CurrMana = maxMana;
            }
        }
        ManaSlider.value = CurrMana;

        CostManabarSlider.value = ManaSlider.value - Cost;
    }
}
