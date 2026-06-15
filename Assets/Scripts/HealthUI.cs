using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static HealthUI Instance;

    public Image healthFill;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateBar(int current, int max)
    {
        healthFill.fillAmount =
            (float)current / max;
    }
}