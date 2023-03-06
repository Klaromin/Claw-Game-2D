using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Data", fileName = "New Game Data")]
public class GameData : ScriptableObject
{
    private void Awake()
    {
        TotalSpawnAmount = smallGemSpawnAmount + mediumGemSpawnAmount + largeGemSpawnAmount + smallGoldSpawnAmount +
                           mediumGoldSpawnAmount + largeGoldSpawnAmount;
    }

    [Header("Gem Information")]
    public int smallGemSpawnAmount = 200;
    public int mediumGemSpawnAmount = 90;
    public int largeGemSpawnAmount = 65;
    [Header("Gold Information")]
    public int smallGoldSpawnAmount = 80;
    public int mediumGoldSpawnAmount = 50;
    public int largeGoldSpawnAmount = 20;
    
    [Header("Total Information")]
    [SerializeField] private int _totalSpawnAmount1;

    public int TotalSpawnAmount
    {
        get => _totalSpawnAmount1;
        set => _totalSpawnAmount1 = value;
    }
}
