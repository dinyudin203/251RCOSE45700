using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // 좌우
        float v = Input.GetAxis("Vertical");   // 앞뒤
        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * speed * Time.deltaTime);
    }
}