using UnityEngine.UI;

public class PurcharePanel : UIPanel
{
    public Text CarNameText;
    public Text CarPriceText;
    public Text CarTonnageText;
    public Text CarMaxSpeedText;
    public Text HintText;
    public void InitializePanel(string Name, int Price, float MaxSpeed, int Tonnage, string hintText)
    {
        CarNameText.text = Name;
        CarPriceText.text = Price.ToString();
        CarMaxSpeedText.text = MaxSpeed.ToString();
        CarTonnageText.text = Tonnage.ToString();
        HintText.text = hintText;
    }
}
