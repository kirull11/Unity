using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}