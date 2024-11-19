using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public Controller player;
    public MONITOR_Interactable monitor;
    [SerializeField] private GameObject VolumeUI;
    [SerializeField] private GameObject MenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (!monitor.isSitting) // TO SOLVE 2+2 = 3 scandura te cheama ma-ta
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
    public void VolumeControl()
    {
        VolumeUI.SetActive(true);
        MenuUI.SetActive(false);
    }
    public void ReturnMenu()
    {
        VolumeUI.SetActive(false);
        MenuUI.SetActive(true);
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
