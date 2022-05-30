using UnityEngine;

public class CargoPhysicsModel : PhysicsModel
{
    [SerializeField] private CargoModel _thisCargo;
    [SerializeField] private float _friction;
    [SerializeField] private float _calmDrag;
    private bool _isConnected;
    private bool _canConnect;
    private Rigidbody2D _connectedBody;
    private SpringJoint2D _springJoint;
    private void Start()
    {
        SetDrag(_calmDrag);
    }
    private void Update()
    {
        ControlConnection();
        if (_isConnected == true)
        {
            SetDrag(_connectedBody.drag);
        }
    }
    private void FixedUpdate()
    {
        ControlSkiding();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isConnected == false)
        {
            if (collision.GetComponentInParent<CarModel>())
            {
                _connectedBody = collision.GetComponentInParent<Rigidbody2D>();
            }
            if(_connectedBody != null)
            {
                CarModel connectedCar = _connectedBody.GetComponentInParent<CarModel>();
                if (connectedCar != null)
                {
                    if (connectedCar.IsSelected() == true)
                    {
                        if (connectedCar.IsCarCanConnect() == true)
                        {
                            _thisCargo.ShowCargoSettingsPanel();
                            _canConnect = true;
                        }
                    }
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _canConnect = false;
        if (_isConnected == false)
        {
            _thisCargo.HideCargoSettingsPanel();
            _connectedBody = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_connectedBody == null)
        {
            _connectedBody = collision.GetComponentInParent<Rigidbody2D>();
        }
    }
    private void ControlConnection()
    {
        if (_isConnected == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DestroyConnection();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && _canConnect == true && _connectedBody !=null)
            {
                CreateConnection(_connectedBody);
            }
        }
    }
    private void CreateConnection(Rigidbody2D conectedBody)
    {
        _isConnected = true;
        _canConnect = false;
        _thisCargo.UpdateState(_isConnected, this);
        _springJoint = gameObject.AddComponent<SpringJoint2D>();
        _springJoint.connectedBody = conectedBody;
        _springJoint.connectedAnchor = new Vector2(0f, -0.6f);
        _springJoint.anchor = new Vector2(0f, 0.8f);
        _springJoint.enableCollision = false;
        _springJoint.autoConfigureConnectedAnchor = false;
        _springJoint.distance = 0.01f;
        _springJoint.dampingRatio = 1f;
        _springJoint.frequency = 0f;
        _springJoint.breakForce = Mathf.Infinity;
        _thisCargo.HideCargoSettingsPanel();
        _thisCargo.PlayTakeSound();
    }
    public void DestroyConnection()
    {
        _isConnected = false;
        _canConnect = true;
        _thisCargo.UpdateState(_isConnected, this);
        Destroy(_springJoint);
        SetDrag(_calmDrag);
        _thisCargo.PlayThrownSound();
    }
    protected override void ControlSkiding()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb2D.velocity, transform.right);
        _rb2D.velocity = forwardVelocity + rightVelocity * _friction;
    }
    private void SetDrag(float drag)
    {
        _rb2D.drag = drag;
    }
    public override float GetVelocityMagnitude()
    {
        return _rb2D.velocity.magnitude;
    }
}
