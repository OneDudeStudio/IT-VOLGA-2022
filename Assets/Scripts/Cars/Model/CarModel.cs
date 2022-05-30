using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarModel : PhysicsModel
{
    [SerializeField] private CarSetting _carStats;
    [SerializeField] private bool _carCanConnect;
    [SerializeField] private bool _isSelected;
    private ResourseManager _resourseManager;
    private TextManager _textManager;
    private CarModel _nextCar;
    private float _moveInput;
    private float _turnInput;
    private float _turnAngle;
    private float _currentSpeed;
    private float _lateralVelocity;
    private bool _isSkid;

    protected override void Awake()
    {
        base.Awake();
        _resourseManager = FindObjectOfType<ResourseManager>();
        _textManager = FindObjectOfType<TextManager>();
    }

    private void FixedUpdate()
    {
        if(_isSelected == true)
        {
            CheckCurrentSpeed();
            Brake();
            Turn();
            ControlSkiding();
        }
    }
    public bool IsCarCanConnect()
    {
        return _carCanConnect;
    }
    public bool IsSelected()
    {
        return _isSelected;
    }
    public void SelectCar()
    {
        _isSelected = true;
        _rb2D.angularDrag = 0.1f;
    }
    public void DeselectCar()
    {
        _isSelected = false;
        _rb2D.drag = 10f;
        _rb2D.angularDrag = 10f;
    }
    private void CheckCurrentSpeed()
    {
        _currentSpeed = _rb2D.velocity.magnitude * 10f;
        if (_currentSpeed > _carStats.MaxSpeed && _moveInput > 0)
        {
            return;
        }
        if (_currentSpeed < -_carStats.MaxSpeed * 0.5f && _moveInput < 0)
        {
            return;
        }
        if (_rb2D.velocity.sqrMagnitude > _carStats.MaxSpeed * _carStats.MaxSpeed && _moveInput > 0)
        {
            return;
        }
        Move();
    }
    public void SetInputVector(Vector2 inputVector)
    {
        _turnInput = inputVector.x;
        _moveInput = inputVector.y;
    }
    private void Move()
    {
        Vector2 engineForceVector = transform.up * _moveInput * _carStats.Speed;
        _rb2D.AddForce(engineForceVector, ForceMode2D.Force);
        _rb2D.angularDrag = 0.1f;
    }
    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb2D.drag = Mathf.Lerp(_rb2D.drag, 5f, Time.fixedDeltaTime);
            _isSkid = true;
        }
        else if (_moveInput == 0)
        {
            _rb2D.drag = Mathf.Lerp(_rb2D.drag, 1f, Time.fixedDeltaTime * 0.5f);
        }
        else if (_moveInput !=0)
        {
            _rb2D.drag = 0f;
            _isSkid = false;
        }
    }
    private void Turn()
    {
       float minSpeedForTurn = (_rb2D.velocity.magnitude / 2);
       minSpeedForTurn = Mathf.Clamp01(minSpeedForTurn);
       _turnAngle -= _turnInput * _carStats.TurnSpeed * minSpeedForTurn;
       _rb2D.MoveRotation(_turnAngle);
       _lateralVelocity = Vector2.Dot(transform.right, _rb2D.velocity);
       if (Mathf.Abs(_lateralVelocity) > 0.5f)
       {
           _isSkid = true;
       }
    }
    protected override void ControlSkiding()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb2D.velocity, transform.right);
        _rb2D.velocity = forwardVelocity + rightVelocity * _carStats.Friction;
    }
    public bool IsSkid()
    {
        return _isSkid;
    }
    public CarSetting GetCarSettings()
    {
        return _carStats;
    }
    public override float GetVelocityMagnitude()
    {
        return _rb2D.velocity.magnitude * 10f;
    }
    public float GetLateralVelocity()
    {
        _lateralVelocity = Vector2.Dot(transform.right, _rb2D.velocity);

        return _lateralVelocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isSelected == true)
        {
            CarModel car = collision.GetComponentInParent<CarModel>();
            if (car != null)
            {
                _nextCar = car;
                _resourseManager.ShowBuyCarPanel(_nextCar);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _resourseManager.HideBuyCarPanel();
        _textManager.HideMessage();
        _nextCar = null;
    }
}
