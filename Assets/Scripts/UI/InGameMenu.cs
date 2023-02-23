using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _winScreen;

    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Castle _castle;

    private void Start() {
        _spawner.OnLevelCompleted += EnableWinScreen;
        _castle.OnLevelFailed += EnableDeathScreen;
    }

    private void OnDestroy() {
        _spawner.OnLevelCompleted -= EnableWinScreen;
        _castle.OnLevelFailed -= EnableDeathScreen;
    }

    private void EnableWinScreen() {
        _winScreen.SetActive(true);
    }

    private void EnableDeathScreen() {
        _deathScreen.SetActive(true);
    }

    public void GotToMenuCallback() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
