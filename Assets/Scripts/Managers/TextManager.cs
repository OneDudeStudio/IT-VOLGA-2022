using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Text _currentSpeedText;
    [SerializeField] private Text _currentCargoText;
    [SerializeField] private Image _messagePanel;
    [SerializeField] private PurcharePanel _pursharePanel;
    [SerializeField] private CargoPanel _cargoPanel;
    [SerializeField] private Text _messageText;
    [SerializeField] private Text _currentCargoCountText;
    private Player _player;
    private GameManager _gameManager;
    private bool _messageAlreadyActive = false;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _gameManager = FindObjectOfType<GameManager>();
        UpdateCargoCountText();
    }
    private void Update()
    {
        if (_player.CurrentCar != null)
        {
            UpdateSpeedText();
        }
    }
    public void UpdateCargoCountText()
    {
        _currentCargoCountText.text = "Осталось отвести " + _gameManager.CargoList.Count.ToString() + " грузов";
    }

    private void UpdateSpeedText()
    {
        _currentSpeedText.text = Mathf.FloorToInt(_player.CurrentCar.GetVelocityMagnitude()).ToString();
    }
    public void UpdateCargoText(string cargoName)
    {
        _currentCargoText.text = "Груз: " + cargoName;
    }
    public void ShowPurcharePanel(string Name, int Price, float MaxSpeed, int Tonnage, string hintText)
    {
        _pursharePanel.InitializePanel(Name, Price, MaxSpeed, Tonnage, hintText);
        _pursharePanel.ShowPanel();
    }
    public void HidePurcharePanel()
    {
        _pursharePanel.HidePanel();
    }
    public void ShowCargoPanel(string Name, string Destination, int Fee, string hintText)
    {
        _cargoPanel.InitializePanel(Name, Destination, Fee, hintText);
        _cargoPanel.ShowPanel();
    }
    public void HideCargoPanel()
    {
        _cargoPanel.HidePanel();
    }
    public void ShowMessage(string message)
    {
        if (_messageAlreadyActive == false )
        {
            _messageText.text = message;
            _messagePanel.gameObject.SetActive(true);  
            _messageAlreadyActive = true;
        }
    }
    public void HideMessage()
    {
        if (_messageAlreadyActive == true)
        {
            _messagePanel.gameObject.SetActive(false);
            _messageAlreadyActive = false;
        }
    }
}
