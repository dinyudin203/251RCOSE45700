using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float hp = 100f;

    void Update()
    {
        if (hp <= 0)
        {
            Respawn();
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("플레이어 체력: " + hp);
    }

    void Respawn()
    {
        transform.position = Vector3.zero;
        hp = 100f;
        Debug.Log("플레이어 리셋!");
    }
}
