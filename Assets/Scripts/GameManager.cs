using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }

    public Text timeText;

    private float survivalTime;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            survivalTime += Time.deltaTime;
        }

        if (timeText != null)
        {
            timeText.text = $"Time: {survivalTime:F1}s";
        }
    }

    public void GameOver()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;
        Debug.Log("Game Over");
    }
}
