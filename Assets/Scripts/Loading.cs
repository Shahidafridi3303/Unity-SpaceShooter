using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    [SerializeField] string sceneToLoad = "MainScene";

    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f);
            loadingBar.fillAmount = progress;

            if (progress >= 1)
            {
                yield return new WaitForSeconds(0.5f);

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }        
    }
}
