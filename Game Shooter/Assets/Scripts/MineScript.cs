using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float DamageAdditional;
    [SerializeField] private float TimeC;

    private float _currentTimeIn;

    void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {
            var hc = other.GetComponent<HealthController>();
            hc.CurrentHealth -= Damage;
            Debug.Log(hc.CurrentHealth);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentTimeIn += Time.deltaTime;
            if (_currentTimeIn >= TimeC)
            {
                var hc = other.GetComponent<HealthController>();
                hc.CurrentHealth -= DamageAdditional;
                Debug.Log(hc.CurrentHealth);
                _currentTimeIn = 0;
            }
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