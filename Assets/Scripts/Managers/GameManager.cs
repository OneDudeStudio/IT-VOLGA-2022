using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<CargoModel> CargoList = new List<CargoModel>();
    [SerializeField] private AudioSource _gameSources;
    [SerializeField] private AudioClip _passCargo;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private CarModel _currentCar;
    private CameraFollow _camera;
    private Player _player;
    private TextManager _textManager;
    private bool _gameIsPaused =false;
    
    private void Awake()
    {
        Time.timeScale = 1f;
        _textManager = FindObjectOfType<TextManager>();
        _camera = FindObjectOfType<CameraFollow>();
        _player = FindObjectOfType<Player>();
        _currentCar = _player.CurrentCar;
        
    }
    private void Update()
    {
        if(_gameIsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else if(_gameIsPaused == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < CargoList.Count; i++)
            {
                CargoList.Remove(CargoList[i]);
            }
            CheckGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _gameIsPaused = true;
        _pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _gameIsPaused = false;
        _pausePanel.SetActive(false);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeCar(CarModel newCar)
    {
        _player.CurrentCar.DeselectCar();
        _player.CurrentCar = null;
        _player.CurrentCar = newCar;
        _player.CurrentCar.SelectCar();
        _currentCar = _player.CurrentCar;
        _camera.FindNewTarget();
    }
    private void CheckGame()
    {
        if(CargoList.Count <= 0)
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
    }
    public void DropCargo(CargoModel cargo)
    {
        CargoList.Remove(cargo);
        _gameSources.PlayOneShot(_passCargo);
        _textManager.UpdateCargoCountText();
        Destroy(cargo.gameObject);
        CheckGame();
    }
}
