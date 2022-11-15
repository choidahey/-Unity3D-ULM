// ���� �ۼ� ��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject player; // �ٶ� �÷��̾� ������Ʈ
    public float xmove = 0;  // X�� ���� �̵���
    public float ymove = 0;  // Y�� ���� �̵���
    public float distance = 10;  // �÷��̾�� ī�޶� ���� �Ÿ�
    private float wheelspeed = 10.0f;  // ����, �ܾƿ� �� ���� ���ǵ�


    void Update()
    {
        CameraMove();

    }

    void CameraMove()
    {
        if (Input.GetMouseButton(1))  //  ���콺 ��Ŭ�� �߿��� ī�޶� ���� ����
        {
            xmove += Input.GetAxis("Mouse X"); // ���콺 �¿� �̵����� xmove �� ����
            ymove -= Input.GetAxis("Mouse Y"); // ���콺 ���� �̵����� ymove �� ����
        }
        transform.rotation = Quaternion.Euler(ymove, xmove, 0); // �̵����� ���� ī�޶��� �ٶ󺸴� ������ ����
        Vector3 reverseDistance = new Vector3(0.0f, -2.0f, distance); // ī�޶� �ٶ󺸴� �չ����� Z ��. �̵����� ���� Z ������� ���� ����

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;  // ��ũ�� ����ؼ� ī�޶� ����, �ܾƿ� ����
        if (distance < 1.0f) distance = 1.0f;
        if (distance > 10.0f) distance = 10.0f;

        transform.position = player.transform.position - transform.rotation * reverseDistance; // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� ����
    }
}