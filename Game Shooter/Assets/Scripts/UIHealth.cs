using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] HealthController healthController;
    [SerializeField] Image image;

    float maxHealth;
    void Start()
    {
        maxHealth = healthController.MaxHealthPoints;
        healthController.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float v)
    {
        image.fillAmount = v / maxHealth;
    }
}
