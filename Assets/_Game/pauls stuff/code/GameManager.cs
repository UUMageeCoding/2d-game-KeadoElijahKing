using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
  public PlatformerController pc;
  public PlayerAttack pa;

    public GameObject gameOverUI;

  // Singleton instance

  private static GameManager _instance;

  

  public static GameManager Instance

  {

    get { return _instance; }
    

  }


    void Awake()

  {

    // Ensure only one instance exists

    if (_instance == null)

    {

      _instance = this;

      DontDestroyOnLoad(gameObject);

    }

    else

    {

      Destroy(gameObject);

    }

  }
    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("title screen");
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
