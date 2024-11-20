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
    private bool VolumeSettingOn = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (!monitor.isSitting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (player.canMove)
                    Pause();
                else if (isPaused && !VolumeSettingOn)
                    Resume();
                else if (VolumeSettingOn)
                    ToggleVolumeMenu(false);
            }
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

    public void ToggleVolumeMenu(bool showVolume)
    {
        VolumeSettingOn = showVolume;
        VolumeUI.SetActive(showVolume);
        MenuUI.SetActive(!showVolume);
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
