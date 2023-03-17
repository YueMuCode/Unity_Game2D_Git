using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NormalHealthBar : MonoBehaviour
{
    public Slider holder;


    public void SetNormalHealthBar(float maxHealth)
    {       
        holder.maxValue = maxHealth;
        holder.value = maxHealth;
    }
    public void UpdateNormalHealthBar(float currentHealth)
    {
        holder.value = currentHealth;
    }
}
