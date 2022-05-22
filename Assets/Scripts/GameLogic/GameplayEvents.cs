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
    /// Enemy dead callback. Provide died AI_Agent on invoke.
    /// </summary>
    public static readonly UnityEvent<AI_Agent> OnEnemyDead = new UnityEvent<AI_Agent>();

    /// <summary>
    /// Player crystall pickup callback. Provide collected crystal on invoke.
    /// </summary>
    public static readonly UnityEvent<Crystal> OnCrystalPickup = new UnityEvent<Crystal>();

    /// <summary>
    /// Crystall destroyed by enemy callback. Provide destroyed crystal on invoke.
    /// </summary>
    public static readonly UnityEvent<Crystal> OnCrystalDestroyed = new UnityEvent<Crystal>();

    /// <summary>
    /// Player get damage callback. Provide damage amount on invoke.
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerGetDamage = new UnityEvent<int>();

    /// <summary>
    /// Player get healing callback. Provide healing amount on invoke.
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerGetHeal = new UnityEvent<int>();

}
