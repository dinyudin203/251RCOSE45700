using UnityEngine;

public class Dagger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("단검 주움!");

            // 범위 내 적들에게 데미지
            Collider[] hitBots = Physics.OverlapSphere(transform.position, 3f);
            foreach (Collider col in hitBots)
            {
                Bot bot = col.GetComponent<Bot>();
                if (bot != null)
                {
                    bot.TakeDamage(40f);
                }
            }

            Destroy(gameObject); // 단검 제거
        }
    }
}