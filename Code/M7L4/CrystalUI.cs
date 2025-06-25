using UnityEngine;
using UnityEngine.UI;

public class CrystalUI : MonoBehaviour
{
    [SerializeField] private Corn _corn;
    [SerializeField] private Text _crystalText;

    private void Update()
    {
        if (_corn && _crystalText)
            _crystalText.text = _corn.Crystals.ToString();
    }
}