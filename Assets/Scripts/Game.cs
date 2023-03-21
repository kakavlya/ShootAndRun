using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private WinScreen _gameWonScreen;
    [SerializeField] private MuzzleEffect _muzzleEffect;
    [SerializeField] private GameObject[] _enemies;

    private void Start()
    {
        _startScreen.Open();
        _player.GameOver += GameOver;
        PauseGame();
        ActivateEnemies();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _gameWonScreen.NextButtonClick += OnNextButtonClick;
        _player.GameOver += GameOver;
        _player.GameWon += GameWon;
        _crosshair.OnShoot += _muzzleEffect.MakeShootEffect;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _gameWonScreen.NextButtonClick -= OnNextButtonClick;
        _player.GameOver -= GameOver;
        _player.GameWon -= GameWon;
        _crosshair.OnShoot -= _muzzleEffect.MakeShootEffect;
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnNextButtonClick()
    {
        _gameWonScreen.Close();
        
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        
        var nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneToLoad);
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _player.Reset();
        _crosshair.gameObject.SetActive(true);
        ActivateEnemies();
    }

    private void ActivateEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.SetActive(true);
            var mover = enemy.GetComponent<EnemyMover>();
            mover.Reset();
            mover.SetTarget(_player.transform);
        }        
    }

    private void GameOver()
    {
        PauseGame();
        _gameOverScreen.Open();
    }

    private void GameWon()
    {
        PauseGame();
        _gameWonScreen.Open();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _crosshair.gameObject.SetActive(false);
    }
}
