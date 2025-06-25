
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Corn _corn;
    [SerializeField] private Text _healthText;

    private void Update()
    {
        if (_corn && _healthText)
            _healthText.text = _corn.Health.ToString();
    }
}
