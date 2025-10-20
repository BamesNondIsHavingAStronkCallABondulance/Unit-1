using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public TMP_Text healthText;
    public Player playerScript;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth()
    {
        int vitals = playerScript.health;

        healthText.text = vitals.ToString();
    }
}
