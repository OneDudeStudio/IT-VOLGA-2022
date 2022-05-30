using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected void Awake()
    {
        HidePanel();
    }
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
