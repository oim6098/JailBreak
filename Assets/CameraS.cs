using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
     public Transform player; // 플레이어 오브젝트의 Transform
    public Vector3 offset; // 플레이어로부터의 카메라 위치 오프셋
    private Quaternion targetRotation; // 목표 회전값
    private float rotationSpeed = 90f; // 초당 회전 속도

    private void Start()
    {
        // 초기 목표 회전값 설정
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        // 플레이어를 따라가도록 카메라 위치 업데이트
        transform.position = player.position + offset;

        // 'O'를 누를 때 왼쪽으로 90도 회전
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopAllCoroutines(); // 현재 진행 중인 회전 애니메이션 중지
            StartCoroutine(RotateCamera(Vector3.up, -90));
        }
        // 'P'를 누를 때 오른쪽으로 90도 회전
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StopAllCoroutines(); // 현재 진행 중인 회전 애니메이션 중지
            StartCoroutine(RotateCamera(Vector3.up, 90));
        }

        // 부드러운 회전을 위한 Slerp 사용
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    IEnumerator RotateCamera(Vector3 axis, float angle)
    {
        // 목표 회전값을 현재 회전값에서 주어진 각도만큼 회전시킨 값으로 업데이트
        targetRotation *= Quaternion.Euler(axis * angle);
        yield return new WaitForSeconds(1); // 1초 대기
    }
}
