
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text _levelText;

    private void Update()
    {
        _levelText.text = LevelController.Level.ToString();
    }
}
