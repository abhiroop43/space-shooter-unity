using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private bool _isGameOver;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
    {
      // reload the current scene
      SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Application.Quit();
    }
  }

  public void GameOver()
  {
    _isGameOver = true;
  }
}
