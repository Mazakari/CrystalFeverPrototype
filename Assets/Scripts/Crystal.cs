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

    #region COLLISION Handler
    private void OnTriggerEnter(Collider other)
    {
        AI_Agent enemy = other.gameObject.GetComponent<AI_Agent>();
        if (enemy)
        {
            GameplayEvents.OnEnemyDead.Invoke(enemy);
            GameplayEvents.OnCrystalDestroyed.Invoke(this);
            return;
        }

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            if (player.CurHealth < player.MaxHealth)
            {
                GameplayEvents.OnPlayerGetHeal.Invoke(_healAmount);
            }

            GameplayEvents.OnCrystalPickup.Invoke(this);
        }
    }
    #endregion
}
