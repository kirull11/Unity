using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetLVL : MonoBehaviour
{
    public int Level;
    private void OnTriggerEnter(Collider colli)
    {
        if (colli.tag == "Player")
        {
            SceneManager.LoadScene("Level 2");
        }
    }
}
