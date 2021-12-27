using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesScript : MonoBehaviour
{
    public int Number;
    public Transform Position;
    public GameObject Zombie1;

    void Start()
    {
        var g01 = Instantiate(Zombie1, Position.position, Quaternion.identity);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
