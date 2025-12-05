using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class HealthBar : MonoBehaviour
{
    private Image healthBarFill;

    void Start()
    {
        healthBarFill = this.GetComponent<Image>();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Calculate the percentage (0.0 to 1.0) and assign it to fillAmount
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
