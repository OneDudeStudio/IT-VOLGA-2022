using UnityEngine;

public class CargoModel : MonoBehaviour
{
    [SerializeField] private DestinationPoint _destinationPoint;
    [SerializeField] private AudioSource _cargoSoursce;
    [SerializeField] private CargoSettings _cargoSetting;
    private ResourseManager _resourseManager;
    private CargoPhysicsModel _cargoPhysicsModel;
    private Player _player;
    private TextManager _textManager;

    private void Awake()
    {
        _cargoPhysicsModel = FindObjectOfType<CargoPhysicsModel>();
        _resourseManager = FindObjectOfType<ResourseManager>();
        _player = FindObjectOfType<Player>();
        _textManager = FindObjectOfType<TextManager>();
    }

    public void PassThisCargo()
    {
        if(_player.CurrentCargo != null)
        {
            _cargoPhysicsModel.DestroyConnection();
            _resourseManager.UpdatePlayerMoney(_cargoSetting.Fee);
            _player.CurrentCargo = null;
            _player.CurrentDestination = null;
        }
    }
    public void UpdateState(bool connected, CargoPhysicsModel thisCargo)
    {
        if(connected == true)
        {
            _player.CurrentCargo = thisCargo;
            _textManager.UpdateCargoText(_cargoSetting.Name);
            _destinationPoint.ActivatePoint();
            _player.CurrentDestination = _destinationPoint.transform;
        }
        else if (connected == false)
        {
            _player.CurrentCargo = null;
            _textManager.UpdateCargoText("Отсутствует");
            _destinationPoint.DeactivatePoint();
            _player.CurrentDestination = null;
        }
    }

    public void ShowCargoSettingsPanel()
    {
        _textManager.ShowCargoPanel(_cargoSetting.Name,_cargoSetting.DestinationTitle, _cargoSetting.Fee, "Нажмите кнопку F для зацепа");
    }
    public void HideCargoSettingsPanel()
    {
        _textManager.HideCargoPanel();
    }
    public void PlayTakeSound()
    {
        _cargoSoursce.PlayOneShot(_cargoSetting.CargoTaken);
    }
    public void PlayThrownSound()
    {
        _cargoSoursce.PlayOneShot(_cargoSetting.CargoThrown);
    }

}
