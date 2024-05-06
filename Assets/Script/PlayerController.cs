using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // 플레이어 움직임 속도

    void Update()
    {
        // 플레이어 이동 로직
        float moveHorizontal = Input.GetAxis("Horizontal"); // A와 D 키
        float moveVertical = Input.GetAxis("Vertical"); // W와 S 키

        // 카메라의 방향에 맞춰서 이동 벡터를 계산
        Vector3 movement = Camera.main.transform.right * moveHorizontal + Camera.main.transform.forward * moveVertical;
        // Y축 이동 제거
        movement.y = 0;

        // 플레이어 위치 업데이트
        transform.position += movement * speed * Time.deltaTime;
    }
}

