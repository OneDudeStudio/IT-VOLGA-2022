using UnityEngine;

public class Player : MonoBehaviour
{
    public CarModel CurrentCar;
    public CargoPhysicsModel CurrentCargo;
    public Transform CurrentDestination;
    void Update()
    {
        CarMoveInput();
    }
    private void CarMoveInput()
    {
        if (CurrentCar != null)
        {
            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            CurrentCar.SetInputVector(inputVector);
        }
    }
}
