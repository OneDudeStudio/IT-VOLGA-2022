using UnityEngine;

public class WheelTrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] _trailRenderers;
    private CarModel _car;

    void Awake()
    {
        _car = GetComponentInParent<CarModel>();
        DisableTrail();
    }
    void Update()
    {
        TrailControl();
    }
    private void TrailControl()
    {
        if (_car.IsSkid())
        {
            EnableTrail();
        }
        else
        {
            DisableTrail();
        }
    }
    private void DisableTrail()
    {
        for (int i = 0; i < _trailRenderers.Length; i++)
        {
            _trailRenderers[i].emitting = false;
        }
    }
    private void EnableTrail()
    {
        for (int i = 0; i < _trailRenderers.Length; i++)
        {
            _trailRenderers[i].emitting = true;
        }
    }
}
