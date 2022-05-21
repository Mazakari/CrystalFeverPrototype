// Roman Baranov 21.05.2022

using UnityEngine;

public class Crystal : MonoBehaviour
{
    #region VARIABLES
    [Min(1)]
    [Header("Player lives heal amount")]
    [SerializeField] private int _healAmount = 1;
    /// <summary>
    /// Healing amount
    /// </summary>
    public int HealAmount { get { return _healAmount; } }
    #endregion

    #region PUBLIC Methods
    public void Pickup()
    {
        // Send callback to add player scores
        GameplayEvents.OnCrystalPickup.Invoke(this);
    }
    #endregion
}
