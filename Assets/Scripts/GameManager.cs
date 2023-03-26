using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private bool _isGameOver = false;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
    {
      // reload the current scene
      SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
  }

  public void GameOver()
  {
    _isGameOver = true;
  }
}
