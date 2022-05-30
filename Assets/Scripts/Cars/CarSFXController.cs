using UnityEngine;

public class CarSFXController : MonoBehaviour
{
    [SerializeField] private AudioSource _tireSkid;
    [SerializeField] private AudioSource _carEngine;
    [SerializeField] private AudioSource _carHit;
    float _enginePitch = 0.5f;
    float _tireSkidPitch;
    CarModel _thisCar;

    void Awake()
    {
        _thisCar = GetComponentInParent<CarModel>();
    }
    void Update()
    {
        EngineSound();
        UpdateTiresScreechingSFX();
    }
    void EngineSound()
    {
        float velocityMagnitude = _thisCar.GetVelocityMagnitude();
        float engineVolume = velocityMagnitude * 0.025f;
        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1.0f);
        _carEngine.volume = Mathf.Lerp(_carEngine.volume, engineVolume, Time.deltaTime * 10);
        _enginePitch = velocityMagnitude * 0.015f;
        _enginePitch = Mathf.Clamp(_enginePitch, 0.5f, 1.5f);
        _carEngine.pitch = Mathf.Lerp(_carEngine.pitch, _enginePitch, Time.deltaTime * 1.5f);
    }
    void UpdateTiresScreechingSFX()
    {
        if (_thisCar.IsSkid())
        {
            float lateralVelocity = _thisCar.GetLateralVelocity();
            _tireSkid.volume = Mathf.Abs(lateralVelocity) * 0.05f;
            _tireSkidPitch = Mathf.Abs(lateralVelocity) * 0.1f;
        }
        else
        {
            _tireSkid.volume = Mathf.Lerp(_tireSkid.volume, 0, Time.deltaTime * 10);
        }
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        float relativeVelocity = collision2D.relativeVelocity.magnitude;
        float volume = relativeVelocity * 0.1f;
        _carHit.pitch = Random.Range(0.95f, 1.05f);
        _carHit.volume = volume;
        if (!_carHit.isPlaying)
            _carHit.Play();
    }


}
