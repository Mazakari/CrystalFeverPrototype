// Roman Baranov 22.05.2022

using UnityEngine.Events;

public class UiEvents
{
    /// <summary>
    ///  Player Hp change callback. Provide new player HP amount on invoke
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerHpChange = new UnityEvent<int>();

    /// <summary>
    ///  Player scores change callback. Provide new player scores amount on invoke
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerScoreChange = new UnityEvent<int>();

    /// <summary>
    ///  Crystals amount change callback. Provide new crystals amount on invoke
    /// </summary>
    public static readonly UnityEvent<int> OnCrystalsCountChange = new UnityEvent<int>();

    /// <summary>
    ///  Enemies amount change callback. Provide new crystals amount on invoke
    /// </summary>
    public static readonly UnityEvent<int> OnEnemiesCountChange = new UnityEvent<int>();

    /// <summary>
    ///  Distance on closest enemy change callback. Provide new closest distance amount on invoke
    /// </summary>
    public static readonly UnityEvent<float> OnClosestEnemyDistanceChange = new UnityEvent<float>();

    /// <summary>
    ///  Distance on closest crystal change callback. Provide new closest distance amount on invoke
    /// </summary>
    public static readonly UnityEvent<float> OnClosestCrystalDistanceChange = new UnityEvent<float>();
}
