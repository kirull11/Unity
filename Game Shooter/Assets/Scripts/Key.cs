using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private DoorType Door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerContrl>();
            pc.GiveKey(Door);
            Destroy(gameObject);
        }
    }
}