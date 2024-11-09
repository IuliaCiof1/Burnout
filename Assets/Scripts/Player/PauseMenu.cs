using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public Controller player;
    public MONITOR_Interactable monitor;
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (!monitor.isSitting) // TO SOLVE
        {
            if (Input.GetKeyDown(KeyCode.Escape) && player.canMove)
                Pause();
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
                Resume();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.canMove = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.canMove = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
