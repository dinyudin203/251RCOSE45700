using UnityEngine;
using UnityEngine.UI;

public class BotHealthUI : MonoBehaviour
{
    public Slider hpBar;
    public Bot bot;

    void Update()
    {
        if (bot != null && hpBar != null)
        {
            hpBar.value = (float)bot.CurrentHP() / bot.maxHP;

        }
    }
}
