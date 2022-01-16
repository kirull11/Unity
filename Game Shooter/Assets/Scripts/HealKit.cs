using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour
{
    [SerializeField] private float healing;
    [HideInInspector] public float CurrentHealth;


    private float _currentTimeIn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var hc = other.GetComponent<HealthController>();
            hc.CurrentHealth += healing;
            Debug.Log(hc.CurrentHealth);
            Destroy(gameObject);
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentTimeIn = 0;
        }
    }
}