using UnityEngine;
using System.Collections;



public class PlayerSkills : MonoBehaviour
{
    public GameObject daggerPrefab;
    public GameObject daggerFlamePrefab; 
    public GameObject qHitEffect;
    float qCooldown = 3f;
    float wCooldown = 2f;
    float eCooldown = 4f;
    float rCooldown = 6f;

    // 현재 타이머
    float qTimer = 0;
    float wTimer = 0;
    float eTimer = 0;
    float rTimer = 0;

    void Update()
    {
        qTimer -= Time.deltaTime;
        wTimer -= Time.deltaTime;
        eTimer -= Time.deltaTime;
        rTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && qTimer <= 0f)
        {
            UseQSkill();
            qTimer = qCooldown;
        }
        if (Input.GetKeyDown(KeyCode.W) && wTimer <= 0f)
        {
            UseWSkill();
            wTimer = wCooldown;
        }
        if (Input.GetKeyDown(KeyCode.E) && eTimer <= 0f)
        {
            UseESkill();
            eTimer = eCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R) && rTimer <= 0f)
        {
            StartCoroutine(UseRSkill());
            rTimer = rCooldown;
        }
    }

    void UseQSkill()
    {
    Debug.Log("Q 스킬 사용!");

    GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
    GameObject nearestBot = null;
    float minDist = Mathf.Infinity;

    foreach (var botObj in bots)
    {
        float dist = Vector3.Distance(transform.position, botObj.transform.position);
        if (dist < minDist)
        {
            minDist = dist;
            nearestBot = botObj;
        }
    }

    if (nearestBot != null && minDist <= 10f)  // 예: 사거리 10
    {
        Bot bot = nearestBot.GetComponent<Bot>();
        if (bot != null)
        {
            Debug.Log("가장 가까운 Bot에게 Q 데미지 적용!");
            bot.TakeDamage(30f);

            // 단검도 생성하고 싶다면 여기에 추가:
            if (daggerPrefab != null)
            {
                Vector3 spawnPos = nearestBot.transform.position +  new Vector3(0f, 0.6f, 0.0f);
                Quaternion rot = Quaternion.Euler(90, 0, 0);
                GameObject dagger = Instantiate(daggerPrefab, spawnPos, rot);
                Destroy(dagger, 1f); // ✅ 1초 후 자동 삭제
            }

            // 이펙트 예시
            if (qHitEffect != null)
            {
                Instantiate(qHitEffect, nearestBot.transform.position, Quaternion.identity);
            }
        }
    }
    else
    {
        Debug.Log("Q 스킬: 사거리 내에 봇 없음!");
    }
    }




    void UseWSkill()
    {
        Debug.Log("W 스킬 사용 - 단검 생성!");
        
        // 위치: 발 밑 (조정 가능)
        Vector3 spawnPos = transform.position + new Vector3(0f, -0.6f, 1.0f);
        Quaternion daggerRotation = Quaternion.Euler(90f, 0f, 0f);

        Instantiate(daggerPrefab, spawnPos, daggerRotation);

        if (daggerFlamePrefab != null)
        {
            GameObject fx = Instantiate(daggerFlamePrefab, spawnPos, Quaternion.identity);
            Destroy(fx, 3f);
        }
    }



    void UseESkill()
{
    Debug.Log("E 스킬 사용 - 단검으로 순간이동!");

    GameObject nearestDagger = FindNearestDagger();
    if (nearestDagger != null)
    {
        // 단검 위치 + 살짝 위로 + 단검 뒤쪽으로 이동
        Vector3 targetPos = nearestDagger.transform.position;
        targetPos.y -= 0.4f;
        Vector3 offset = -nearestDagger.transform.forward * 1.0f;
        transform.position = targetPos + offset;

        // 주변 봇 공격
        Collider[] hitBots = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in hitBots)
        {
            Bot bot = col.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(60f);
            }
        }
    }
    else
    {
        Debug.Log("단검 없음 → 봇에게 순간이동 시도");

        GameObject nearestBot = FindNearestBot();
        if (nearestBot != null)
        {
            Vector3 targetPos = nearestBot.transform.position;
            Vector3 offset = -nearestDagger.transform.forward * 1.0f;
            targetPos.y -= 0.4f;
            transform.position = targetPos + offset;
            Bot bot = nearestBot.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(50f);
            }
        }
    }
}


    IEnumerator UseRSkill()
    {
        Debug.Log("R 스킬 사용!");

        float duration = 2.5f;
        float interval = 0.5f;
        float timer = 0f;

        while (timer < duration)
        {
            // 캐릭터 회전
            transform.Rotate(0f, 360f * (interval / duration), 0f);

            // 주변 적 데미지
            Collider[] hitBots = Physics.OverlapSphere(transform.position, 3f);
            foreach (Collider col in hitBots)
            {
                Bot bot = col.GetComponent<Bot>();
                if (bot != null)
                {
                    bot.TakeDamage(15f);
                }
            }

            timer += interval;
            yield return new WaitForSeconds(interval);
        }
    }


    GameObject FindNearestBot()
    {
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject bot in bots)
        {
            float dist = Vector3.Distance(transform.position, bot.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = bot;
            }
        }

        return nearest;
    }
    GameObject FindNearestDagger()
    {
        GameObject[] daggers = GameObject.FindGameObjectsWithTag("Dagger");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject dagger in daggers)
        {
            float dist = Vector3.Distance(transform.position, dagger.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = dagger;
            }
        }

        return nearest;
    }
}
