using UnityEngine;

public class DaggerPassive : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float damage = 50f;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist < 0.8f)
        {
            // 폭발!
            Collider[] hitBots = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider col in hitBots)
            {
                Bot bot = col.GetComponent<Bot>();
                if (bot != null)
                {
                    bot.TakeDamage(damage);
                }
            }

            // 이펙트 나중에 추가 가능
            Destroy(gameObject); // 단검 삭제
        }
    }
}
