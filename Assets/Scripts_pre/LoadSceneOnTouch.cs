using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    [SerializeField] private int sceneIndex = -1;
    [SerializeField] private bool detectCursor;
    
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c == null || !c.CompareTag("Player")) return;
        Switch();
    }

    private void OnMouseDown()
    {
        if (!detectCursor) return;
        Switch();
    }

    private void Switch()
    {
        var scene = sceneIndex switch
        {
            -1 => SceneManager.GetActiveScene().buildIndex,
            -2 => SceneManager.GetActiveScene().buildIndex + 1,
            _ => sceneIndex
        };
        SceneManager.LoadScene(scene);
    }
}