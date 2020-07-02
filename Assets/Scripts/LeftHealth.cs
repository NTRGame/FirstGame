using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftHealth : MonoBehaviour
{
    public Image healthPointImage;
    public Image healthPointEffect;
    private PlayerControl Player;
    
    private void Awake() 
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    void Update() 
    {
        healthPointImage.fillAmount = Player.currentHp / Player.maxHp;
        if(healthPointEffect.fillAmount < healthPointImage.fillAmount)
        {
            healthPointEffect.fillAmount -= 0.003f;
        }
        else
        {
            healthPointEffect.fillAmount = healthPointImage.fillAmount;
        }
    }
}
