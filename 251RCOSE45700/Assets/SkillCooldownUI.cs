using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Slider qSlider;
    public Slider wSlider;
    public Slider eSlider;
    public Slider rSlider;

    public float qCooldown = 3f;
    public float wCooldown = 2f;
    public float eCooldown = 4f;
    public float rCooldown = 6f;

    float qTimer = 0;
    float wTimer = 0;
    float eTimer = 0;
    float rTimer = 0;

    void Update()
    {
        // Q
        if (Input.GetKeyDown(KeyCode.Q) && qTimer <= 0)
        {
            qTimer = qCooldown;
        }
        if (qTimer > 0)
        {
            qTimer -= Time.deltaTime;
            qSlider.value = 1f - (qTimer / qCooldown);
        }
        else qSlider.value = 1f;

        // W
        if (Input.GetKeyDown(KeyCode.W) && wTimer <= 0)
        {
            wTimer = wCooldown;
        }
        if (wTimer > 0)
        {
            wTimer -= Time.deltaTime;
            wSlider.value = 1f - (wTimer / wCooldown);
        }
        else wSlider.value = 1f;

        // E
        if (Input.GetKeyDown(KeyCode.E) && eTimer <= 0)
        {
            eTimer = eCooldown;
        }
        if (eTimer > 0)
        {
            eTimer -= Time.deltaTime;
            eSlider.value = 1f - (eTimer / eCooldown);
        }
        else eSlider.value = 1f;

        // R
        if (Input.GetKeyDown(KeyCode.R) && rTimer <= 0)
        {
            rTimer = rCooldown;
        }
        if (rTimer > 0)
        {
            rTimer -= Time.deltaTime;
            rSlider.value = 1f - (rTimer / rCooldown);
        }
        else rSlider.value = 1f;
    }
}
