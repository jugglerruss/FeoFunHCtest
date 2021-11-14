using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform _losePanel;
    [SerializeField] private Transform _winPanel;

    private bool _isPlay;
    public bool IsPlay => _isPlay;
    public void StartGame()
    {
        _isPlay = true;
    }
    public void StopGame()
    {
        _isPlay = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
    public void Lose()
    {
        _losePanel.gameObject.SetActive(true);
        StopGame();
    }
    public void Win()
    {
        _winPanel.gameObject.SetActive(true);
        StopGame();
    }
}
