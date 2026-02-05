using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool gameStopped = false;
    public GameObject menuUI;
    public UnityEngine.UI.Slider soundSlider;
    public MonoBehaviour mouseLookScript;
    void Start()
    {
        soundSlider.value = AudioListener.volume;
        soundSlider.onValueChanged.AddListener(SetVolume);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStopped)
            {
                Resume();
                mouseLookScript.enabled = true;
            }
            else
            {
                Pause();
                mouseLookScript.enabled = false;
            }
        }
    }
    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        gameStopped = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        gameStopped = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
