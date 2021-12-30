using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesScript : MonoBehaviour
{
    public int Number;
    public Transform Position;
    public GameObject zombie1;

    void Start()
    {
        var g01 = Instantiate(zombie1, Position.position, Quaternion.identity);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
