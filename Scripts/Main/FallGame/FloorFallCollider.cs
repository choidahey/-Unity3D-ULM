using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFallCollider : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        // CompareTag()�� ��������ν� ����ȭ
        if (collision.collider.gameObject.CompareTag("FallFloor"))  // �±װ� FallFloor�� ������Ʈ�� �浹�� �� �۵�, ������ ������� ��
        {
            Debug.Log("��Ͽ� ����");
            Destroy(collision.gameObject, 1f);  // 1�� �ڿ� ��� ���ֱ�
        }

        if (collision.collider.gameObject.CompareTag("Ground"))  // ��Ƶ� ������� �ʴ� ������ ��
        {
            Debug.Log("Ground�� ����");
        }

        if (collision.collider.gameObject.CompareTag("Frame"))
        {
            transform.position = new Vector3(-500.3f, -951.07f, 904.94f);  // �������� �� ���� ��ġ�� ���ư���
        }
    }
}
