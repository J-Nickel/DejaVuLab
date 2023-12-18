using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Image timeBar;
    private float timeLeft;

    private void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = timeLimit;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / timeLimit;
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}