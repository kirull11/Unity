using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dooor : MonoBehaviour
{
    [SerializeField] private DoorType Door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerContrl>();
            if (pc.HasKey(Door))
            {
                //Open();
                Destroy(gameObject);
            }

        }
    }

    private void Open()
    {

    }
}