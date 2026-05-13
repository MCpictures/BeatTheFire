using UnityEngine;

public class WindowScore : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] int scoreAmount;

    [Header("Layer information")]
    [SerializeField] LayerMask playerLayer;

    [Header("BaseClasses")]
    [SerializeField] ScoreManager scoreManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            InnocentActivator innocentActivator = other.gameObject.GetComponentInParent<InnocentActivator>();
            if (innocentActivator == null || !innocentActivator.IsHoldingInnocent) return;
            innocentActivator.DeactivateItemDisplay();
            scoreManager.Scored(scoreAmount);
        }
    }

}
