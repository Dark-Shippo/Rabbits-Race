using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MenuObjects;
    public GameObject GameOverMenu;
    public bool menuOpen = false;
    [SerializeField]
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            MenuObjects.gameObject.SetActive(true);
            menuOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen)
        {
            MenuObjects.gameObject.SetActive(false);
            menuOpen = false;
        }
        if (player.isDead && !menuOpen)
        {
            GameOverMenu.gameObject.SetActive(true);
            menuOpen = true;
        }
    }

    public void TestArea()
    { 
        SceneManager.LoadScene("TestArea", LoadSceneMode.Single); 
    }
    public void QuitGame()
    {

    }
    public void ContinueGame()

    {
        MenuObjects.gameObject.SetActive(false);
        menuOpen = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }
    public void ReturnHub()

    {
        SceneManager.LoadScene("HUB", LoadSceneMode.Single);
    }
    public void RestartLevel()

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

}

