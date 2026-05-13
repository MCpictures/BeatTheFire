using UnityEngine;

public class InnocentActivator : MonoBehaviour
{
    [SerializeField] GameObject itemDisplay;

    bool isHoldingInnocent = false;

    public void ActivateItemDisplay()
    {
        itemDisplay.SetActive(true);
        isHoldingInnocent = true;
    }

    public void DeactivateItemDisplay()
    {
        itemDisplay.SetActive(false);
        isHoldingInnocent = false;
    }

    public bool IsHoldingInnocent { get { return isHoldingInnocent;} }
}