using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private int _healthGradePrice;
    [SerializeField] private Corn _corn;
    [SerializeField] private Text _healthGradePriceText;

    private int _healthGrade;

    private void Awake()
    {
        _healthGrade = PlayerPrefs.GetInt("healthGrade", 0);

        if (!_healthGradePriceText) enabled = false;
    }

    private void Update()
    {
        _healthGradePriceText.text = _healthGradePrice.ToString();
    }

    public void OnClickUpgradeHealth()
    {
        if (_corn.TrySpendCrystals(_healthGradePrice))
        {
            _healthGrade++;
            PlayerPrefs.SetInt("healthGrade", _healthGrade);
            PlayerPrefs.Save();
            _corn.RecalculateHealth();
        }
    }
}