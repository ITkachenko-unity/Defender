
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Corn _corn;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    public static bool IsFinished { get; private set; }
    public static int Level { get; private set; } = 1;

    private void Start()
    {
        Level = PlayerPrefs.GetInt("level", 1);
        IsFinished = false;
    }

    private void Update()
    {
        if (IsFinished)
        {
            return;
        }

        CheckGameState();
    }

    public void StartGame()
    {
        _spawner.InitSpawner(Level);
        _victoryPanel.SetActive(false);
        IsFinished = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void CheckGameState()
    {
        if (_spawner.SpawnCounter <= 0 && Enemy.ActiveEnemies.Count == 0)
        {
            Victory();
        }

        if (_corn.Health <= 0)
        {
            Defeat();
        }
    }

    private void Victory()
    {
        IsFinished = true;
        Level++;
        _victoryPanel.SetActive(true);
        SaveController.SaveLevel(Level);
    }

    private void Defeat()
    {
        IsFinished = true;
        _defeatPanel.SetActive(true);
    }
}
