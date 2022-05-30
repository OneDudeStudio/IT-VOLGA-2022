using UnityEngine;
using UnityEngine.UI;

public class CargoPanel : UIPanel
{
    public Text CargoNameText;
    public Text CargoFeeText;
    public Text DestinationText;
    public Text HintText;

    public void InitializePanel(string Name, string Destination, int Fee, string hintText)
    {
        CargoNameText.text = Name;
        DestinationText.text = Destination;
        CargoFeeText.text = Fee.ToString();
        HintText.text = hintText;
    }
}
