using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private GameObject levelButton;
        [SerializeField] private int hSize = 7;

        private void Start()
        {
            var count = SceneManager.sceneCountInBuildSettings;
            for (var i = 1; i < count; i++)
            {
                var x = 60 * ((i - 1) % hSize) + 10;
                var y = -60 * ((i - 1) / hSize) - 10;

                var obj = Instantiate(levelButton, new Vector3(x, y, 0), Quaternion.identity);

                var text = obj.GetComponentInChildren<TMP_Text>();
                text.text = i + "";

                var ls = obj.GetComponent<LaunchScene>();
                ls.SceneIndex = i;

                obj.transform.SetParent(gameObject.transform, false);
            }
        }
    }
}