using UnityEngine;

[CreateAssetMenu(menuName = "Cargo Settings")]
public class CargoSettings : ScriptableObject
{
    public string Name;
    public int Fee;
    public string DestinationTitle;
    public float TimeToDelivery;
    public AudioClip CargoTaken;
    public AudioClip CargoThrown;
}
