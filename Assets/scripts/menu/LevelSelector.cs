using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private GameObject levelButton;
        [SerializeField] private int hSize = 7;
        [SerializeField] private int shift = 24;
        [SerializeField] private int objSize = 50;

        private void Start()
        {
            var count = SceneManager.sceneCountInBuildSettings;

            var x = shift;
            var y = -shift;
            for (var i = 1; i < count; i++)
            {
                var obj = Instantiate(levelButton, new Vector3(x, y, 0), Quaternion.identity);

                var text = obj.GetComponentInChildren<TMP_Text>();
                text.text = i + "";

                var ls = obj.GetComponent<LaunchScene>();
                ls.SceneIndex = i;

                obj.transform.SetParent(gameObject.transform, false);

                x = i % hSize == 0 ? shift : x + (objSize + shift);
                y = i % hSize == 0 ? y - (objSize + shift) : y;
            }
        }
    }
}