using UnityEngine;
using UnityEngine.SceneManagement;

namespace menu
{
    public class LaunchScene : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;

        public void Run()
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public int SceneIndex
        {
            set => sceneIndex = value;
        }
    }
}