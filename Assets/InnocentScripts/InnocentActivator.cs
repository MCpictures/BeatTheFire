using UnityEngine;

public class InnocentActivator : MonoBehaviour
{
    [SerializeField] GameObject itemDisplay;

    public void ActivateItemDisplay()
    {
        itemDisplay.SetActive(true);
    }

    public void DeactivateItemDisplay()
    {
        itemDisplay.SetActive(false);
    }
}