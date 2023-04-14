using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        
        _backButton.OnClickAsObservable().Subscribe(_ =>
        gameObject.SetActive(false));
        _exitButton.OnClickAsObservable().Subscribe(_ =>
        Application.Quit());
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
