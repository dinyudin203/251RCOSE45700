using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    public float maxHP = 100f;
    float hp;

    public GameObject healthBarCanvas; // 🩸 체력바 연결용

    void Start()
    {
        hp = maxHP;

        // 시작할 때 체력바 켜기
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(true);
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        // 몸 숨기기
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }

        // 체력바 숨기기
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(false);

        // 충돌 꺼주기
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }

        yield return new WaitForSeconds(5f);

        // 체력 회복
        hp = maxHP;

        // 다시 몸 보이기
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }

        // 충돌 다시 켜기
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }

        // 체력바 다시 보이기
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(true);
    }

    public float CurrentHP()
    {
        return hp;
    }
}
