using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private AsyncOperation asyncLoad;

    void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    IEnumerator LoadGameSceneAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        // Desactivar el menú principal
        gameObject.SetActive(false);

        // Permitir la activación de la escena del juego
        asyncLoad.allowSceneActivation = true;

        // Esperar a que la escena se active completamente
        yield return new WaitUntil(() => asyncLoad.isDone && asyncLoad.progress >= 0.9f);

        // Iniciar el juego desde el GameManager
        GameManager.instance.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
