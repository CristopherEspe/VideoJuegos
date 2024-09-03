using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void OnEnemyDestroyed(int amount)
    {
        AddScore(amount);
    }

    public void OnPlayerDeath()
    {
        // Manejar la lógica de la muerte del jugador aquí (ej. reiniciar el juego, mostrar pantalla de game over, etc.)
    }
}
