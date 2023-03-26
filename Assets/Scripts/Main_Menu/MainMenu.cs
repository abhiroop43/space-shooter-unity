using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void LoadGame()
  {
    // load main game scene
    SceneManager.LoadScene(1);
  }
}
