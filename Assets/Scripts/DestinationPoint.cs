using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPoint : MonoBehaviour
{
    [SerializeField] private CargoModel _currentCargo;
    [SerializeField] private BoxCollider2D _trigger;
    [SerializeField] private SpriteRenderer _pointSprite;
    private bool _isActive = false;
    private bool _canAcceptCargo;
    private TextManager _textManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _textManager = FindObjectOfType<TextManager>();
    }
    private void Update()
    {
        AcceptCargo();
    }

    private void AcceptCargo()
    {
        if (_canAcceptCargo == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _currentCargo.PassThisCargo();
                _gameManager.DropCargo(_currentCargo);
                _currentCargo = null;
                _canAcceptCargo = false;
                DeactivatePoint();

            }
        }
    }
    IEnumerator ActivePoint()
    {
        Color startColor = _pointSprite.color;
        while (_isActive)
        {
            float t = 0;
            float animationDuration = 2f;
            while (t < 1)
            {
                _pointSprite.color = Color.Lerp(Color.white, Color.yellow, t);
                t += Time.fixedDeltaTime / animationDuration;
                yield return null;
            }
            t = 0;
            while (t < 1)
            {
                _pointSprite.color = Color.Lerp(Color.yellow, Color.white, t);
                t += Time.fixedDeltaTime / animationDuration;
                yield return null;
            }
        }
        _pointSprite.color = startColor;
    }
    public void ActivatePoint()
    {
        _trigger.enabled = false;
        _isActive = true;
        _trigger.enabled = true;
        StartCoroutine(ActivePoint());
    }
    public void DeactivatePoint()
    {
        _isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_currentCargo == null)
        {
            _currentCargo = collision.GetComponentInParent<CargoModel>();
            if (_currentCargo != null)
            {
                if (_isActive == true)
                {
                    _canAcceptCargo = true;
                    _textManager.ShowMessage("Нажмите Enter чтобы сдать груз");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<CargoModel>() != null)
        {
            _currentCargo = null;
            _canAcceptCargo = false;
            _textManager.HideMessage();
        }
    }
}
