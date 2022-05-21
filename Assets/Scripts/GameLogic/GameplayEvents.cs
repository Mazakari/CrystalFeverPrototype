// Roman Baranov 21.05.2022

using UnityEngine;
using UnityEngine.Events;

public class GameplayEvents
{
    /// <summary>
    /// Player start moving callback
    /// </summary>
    public static readonly UnityEvent OnPlayerMove = new UnityEvent();

    /// <summary>
    /// Player idle callback
    /// </summary>
    public static readonly UnityEvent OnPlayerIdle = new UnityEvent();

    /// <summary>
    /// Player dead callback
    /// </summary>
    public static readonly UnityEvent OnPlayerDead = new UnityEvent();

    /// <summary>
    /// Enemy dead callback
    /// </summary>
    public static readonly UnityEvent OnEnemyDead = new UnityEvent();

    /// <summary>
    /// Player crystall pickup callback.
    /// </summary>
    public static readonly UnityEvent<Crystal> OnCrystalPickup = new UnityEvent<Crystal>();

    /// <summary>
    /// Player get damage callback. Provide damage amount on invoke.
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerGetDamage = new UnityEvent<int>();

    /// <summary>
    /// Player get healing callback. Provide healing amount on invoke.
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerGetHeal = new UnityEvent<int>();

}
