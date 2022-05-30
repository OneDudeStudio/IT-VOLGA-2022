using UnityEngine;
using UnityEngine.UI;

public class ResourseManager : MonoBehaviour
{
    public bool CanBuyThisCar;
    [SerializeField] private Text _playerMoneyText;
    [SerializeField] private int _playerMoney;
    private GameManager _gameManager;
    private TextManager _textManager;
    private Player _player;
    private CarModel _nextCar;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _textManager = FindObjectOfType<TextManager>();
        _player = FindObjectOfType<Player>();
        UpdatePlayerMoney(0);
    }
    private void Update()
    {
        TryToBuyCar(_nextCar, _player.CurrentCar.GetCarSettings().Price);
    }
    public void ShowBuyCarPanel(CarModel nextCar)
    {
        int currentCarPrice = _player.CurrentCar.GetCarSettings().Price;
        _nextCar = nextCar;
        CarSetting nextCarSettings = nextCar.GetCarSettings();
        if (_player.CurrentCargo != null)
        {
            CanBuyThisCar = false;
            _textManager.ShowPurcharePanel(nextCarSettings.Name, nextCarSettings.Price, nextCarSettings.MaxSpeed, nextCarSettings.Tonnage, "Вы доставляете груз и не можете сейчас купить эту машину.");
        }
        else if (_player.CurrentCargo == null)
        {
            if (CheckMoney(nextCarSettings.Price - currentCarPrice) == true)
            {
                CanBuyThisCar = true;
                _textManager.ShowPurcharePanel(nextCarSettings.Name, nextCarSettings.Price, nextCarSettings.MaxSpeed, nextCarSettings.Tonnage, "Нажмите Enter чтобы купить.");
            }
            else
            {
                CanBuyThisCar = false;
                _textManager.ShowPurcharePanel(nextCarSettings.Name, nextCarSettings.Price, nextCarSettings.MaxSpeed, nextCarSettings.Tonnage, "У вас пока недостаточно денег на покупку.");
            }
        }
        else
        {
            CanBuyThisCar = false;
        }
    }
    public void HideBuyCarPanel()
    {
        _textManager.HidePurcharePanel();
        CanBuyThisCar = false;
        _nextCar = null;
    }
    public void TryToBuyCar(CarModel nextCar, int currentCarPrice)
    {
        if (Input.GetKeyDown(KeyCode.Return) && CanBuyThisCar == true)
        {
            _player.CurrentCar.enabled = false;
            _gameManager.ChangeCar(nextCar);
            _player.CurrentCar.enabled = true;
            UpdatePlayerMoney(-(nextCar.GetCarSettings().Price - currentCarPrice));
            _textManager.HidePurcharePanel();
        }
    }
    private bool CheckMoney(int value)
    {
        if (_playerMoney >= value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdatePlayerMoney(int value)
    {
        _playerMoney += value;
        UpdateMoneyText();
    }
    private void UpdateMoneyText()
    {
        _playerMoneyText.text = _playerMoney.ToString();
    }
}
