using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBarManager : MonoBehaviour
{
    public Slider HP;
    public void SetMaxHealth(float health)  
    {
        HP.maxValue  = health;
        HP.value = health;
    }
    public void SetHealth(float health)
    {
        HP.value = health;
    }
}
