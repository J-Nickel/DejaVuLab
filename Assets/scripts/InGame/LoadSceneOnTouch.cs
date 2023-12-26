using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGame
{
    public class LoadSceneOnTouch : MonoBehaviour
    {
        private enum LoadType
        {
            Current,
            Next,
            Menu
        }

        [SerializeField] private LoadType loadType = LoadType.Next;
        [SerializeField] private bool detectCursor;
        [SerializeField] private bool detectPlayer;

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (!detectPlayer || c == null || !c.CompareTag("Player")) return;
            Load();
        }

        private void OnMouseDown()
        {
            if (!detectCursor) return;
            Load();
        }

        private void Load()
        {
            var scene = loadType switch
            {
                LoadType.Current => SceneManager.GetActiveScene().buildIndex,
                LoadType.Next => SceneManager.GetActiveScene().buildIndex + 1,
                LoadType.Menu => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
            SceneManager.LoadScene(scene);
        }
    }
}