using UnityEngine;
[CreateAssetMenu(menuName = "Car Settings")]
public class CarSetting : ScriptableObject
{
    public string Name;
    public int Price;
    public int Tonnage;
    public float Speed;
    public float MaxSpeed;
    public float TurnSpeed;
    public float Friction;
}
