using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothFactor;
    public Transform Target;
    private Player _playerCurrentCar;

    private void Awake()
    {
        _playerCurrentCar = GetComponentInChildren<Player>();
        FindNewTarget();
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Target != null)
        {
            Vector2 targetPosition = Target.transform.position + _offset;
            Vector2 smoothPosition = Vector2.Lerp(transform.position, targetPosition, _smoothFactor * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }
    public void FindNewTarget()
    {
        Target = _playerCurrentCar.CurrentCar.transform;
    }
}
