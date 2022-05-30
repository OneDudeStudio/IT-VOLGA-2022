using UnityEngine;

public abstract class PhysicsModel : MonoBehaviour
{
    protected Rigidbody2D _rb2D;
    protected virtual void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    protected abstract void ControlSkiding();
    public abstract float GetVelocityMagnitude();
}
