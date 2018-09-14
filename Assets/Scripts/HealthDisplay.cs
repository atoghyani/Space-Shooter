using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {

    TextMeshProUGUI healthText;
    Player player;
    [SerializeField] float currentHealth;
    float totalHealth;

    // Use this for initialization
    void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
        totalHealth= player.GetPlayerHealth();
        currentHealth= totalHealth/totalHealth*100;
        healthText.text = currentHealth.ToString() + "%";


    }

    // Update is called once per frame
    void Update()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
        currentHealth = player.GetPlayerHealth()/ totalHealth * 100;
        healthText.text = currentHealth.ToString() + "%";
    }
}
