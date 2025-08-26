using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Slider _loadSlider;
    [SerializeField] private GameObject _loadScreen;

    private void Start()
    {
        _loadScreen.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        GameManager.Instance.PauseGame();
        AudioManager.Instance.PlayUISFXClip(1);
        _loadScreen.SetActive(true);
        StartCoroutine(WaitThenLoad(sceneIndex));
    }

    private IEnumerator WaitThenLoad(int sceneIndex)
    {
        yield return null; // Let Unity render the load screen
        StartCoroutine(LoadAsync(sceneIndex));
    }

    public void ContinueGame()
    {
        Debug.Log("Continue GAme");
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            if (_loadSlider != null)
                _loadSlider.value = operation.progress < 0.9f ? operation.progress : 1f;

            yield return null;
        }
    }
}
