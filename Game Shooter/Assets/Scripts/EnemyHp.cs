using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHp : MonoBehaviour
{
    public int health;
  
    void Update()
    {
        if (health <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Menu");
    }
}
