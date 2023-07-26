using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public float maxMana;
    public float RegenSpeed;
    public float Cost = 0;

    public Slider ManaSlider;
    public Slider CostManabarSlider;

    public Image ManaImage;

    public Color ManaColor;
    public Color NotEnoughManaColor;

    public float CurrMana;

    public bool DisplayMana;

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

    public void UpdateCost(float Amount)
    {
        Cost = Amount;
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
        if(CurrMana < Cost)
        {
            ManaImage.color = NotEnoughManaColor;
        }
        else 
        {
            ManaImage.color = ManaColor;
        }

        ManaSlider.value = CurrMana;
        CostManabarSlider.value = CurrMana - Cost;
    }
}
