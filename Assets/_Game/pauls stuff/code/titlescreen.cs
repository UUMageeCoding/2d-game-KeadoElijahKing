using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class titlescreen : MonoBehaviour
{


    public string newGameScene;



    public GameObject startGame;

    public GameObject controlsMenu;

    public GameObject quitGame;

    public GameObject loadGame;



    public void NewGame()

    {

        SceneManager.LoadScene(newGameScene);

    }



    public void QuitGame()

    {

        Application.Quit();

    }



    void ResetSelection()

    {

        EventSystem.current.SetSelectedGameObject(null);

    }



    public void OpenMenu(GameObject menuToOpen, GameObject firstButton)

    {

        ResetSelection(); // Deselect current selection



        // Disable all menus

        startGame.SetActive(false);

        controlsMenu.SetActive(false);



        // Enable the selected menu

        menuToOpen.SetActive(true);



        // Set the first selected button for navigation

        if (firstButton != null)

        {

            EventSystem.current.SetSelectedGameObject(firstButton);

        }

    }
}
