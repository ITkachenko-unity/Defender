
using UnityEngine;

public class Corn : MonoBehaviour
{
    [field: SerializeField] public int StartHealth { get; private set; } = 10;
    [field: SerializeField] public int HealthPerUpgrade { get; private set; } = 2;

    public int Health { get; private set; }
    public int Crystals { get; private set; }

    private void Awake()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        int healthGrade = PlayerPrefs.GetInt("healthGrade", 0);
        Health = StartHealth + (HealthPerUpgrade * healthGrade);
        Crystals = PlayerPrefs.GetInt("crystals", 0);
    }

    public void TakeDamage()
    {
        if (Health <= 0) return;
        Health = Mathf.Max(Health - 1, 0);
    }

    public void AddCrystals(int amount)
    {
        Crystals += amount;
        SaveController.SaveCrystals(Crystals);
    }

    public bool TrySpendCrystals(int amount)
    {
        if (Crystals < amount) return false;
        Crystals -= amount;
        SaveController.SaveCrystals(Crystals);
        return true;
    }

    public void RecalculateHealth()
    {
        int healthGrade = PlayerPrefs.GetInt("healthGrade", 0);
        Health = StartHealth + (HealthPerUpgrade * healthGrade);
    }
}
