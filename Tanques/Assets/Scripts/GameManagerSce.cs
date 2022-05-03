using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSce : MonoBehaviour
{
  public void LoadScene()
  {
        SceneManager.LoadScene(1);
  }
  public void ExitSGame()
  {
        Application.Quit();
  }
}

