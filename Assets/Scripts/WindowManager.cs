using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private MonoBehaviour[] componentsToDisable;//засовываем скрипты, которые нужно отключить при паузе, если у вас Time.scale не будет зануляться

    private bool _pauseGame;

    public static WindowManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseWindow.SetActive(false);
        Time.timeScale = 1f;
        _pauseGame = false;
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = true;
        }
    }

    public void Pause()
    {
        pauseWindow.SetActive(true);
        Time.timeScale = 0f;
        _pauseGame = true;
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        loseWindow.SetActive(true);
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        winWindow.SetActive(true);
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }

    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

}
