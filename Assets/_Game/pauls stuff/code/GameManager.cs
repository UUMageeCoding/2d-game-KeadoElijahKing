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

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    private void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
        SceneManager.LoadScene("title screen");
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
