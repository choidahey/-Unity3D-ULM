using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInMap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainFall"))   // ���� �ʿ��� �������� �� �������� ������
        {
            transform.position = new Vector3(0, 2, 0);
        }
    }
    private void OnTriggerStay(Collider other)   // ���̵� �� �ƿ� �κ� ĵ���� Ʈ����
    {
        switch (other.gameObject.tag)
        {
            // position in
            case "LibraryPosition":   // ���� �ݶ��̴� �ڽ� �±� (position out)
                {
                    Invoke("EnterFade_Lib", 0.7f);
                    break;
                }
            case "GymPosition":
                {
                    Invoke("EnterFade_Gym", 0.7f);
                    break;
                }
            case "LecturePosition":
                {
                    Invoke("EnterFade_Lect", 0.7f);
                    break;
                }
            case "HeadQuaterPosition":
                {
                    Invoke("EnterFade_Head", 0.7f);
                    break;
                }
            // position out ���ӿ��� �����°� �̾�ϱ�
            case "ExitInterview":
                {
                    Invoke("ExitFade_Head", 0.7f);
                    break;
                }
            case "ExitFallGame":
                {
                    Invoke("ExitFade_Lect", 0.7f);
                    break;
                }
            case "ExitMazeGame":
                {
                    Invoke("ExitFade_Lib", 0.7f);
                    break;
                }
            case "ExitHuddleGame":
                {
                    Invoke("ExitFade_Gym", 0.7f);
                    break;
                }
        }
    }

    // position in
    void EnterFade_Lib()
    {
        transform.position = new Vector3(-1.04f, -663.1f, -44.5f); // �������� �̷� ������ �̵�
    }
    void ExitFade_Lib()
    {
        transform.position = new Vector3(-200, 1.83f, -254);  // �������� �� ������ �̵�
    }

    void EnterFade_Gym()
    {
        transform.position = new Vector3(-7.17f, -1355.22f, -87.6f);  // ü������ ��� ������ �̵�
    }
    void ExitFade_Gym()
    {
        transform.position = new Vector3(-105, 0.88f, -74.84f);;  // ü������ �� �� �̵�
        //transform.localScale = gymPos.position += new Vector3(0, 0, -10);
    }

    void EnterFade_Lect()
    {
        transform.position = new Vector3(-500.3f, -951, 904);  // ���հ��ǵ��� �ٴ� �������� ���Ӹ����� �̵�
    }
    void ExitFade_Lect()
    {
        transform.position = new Vector3(-400.5f, 14.63f, -78); // ���հ��ǵ��� �� ������ �̵�
    }

    void EnterFade_Head()
    {
        transform.position = new Vector3(1.7f, -488.5f, -6.69f);  // ���� ���� ���������� �̵�
    }
    void ExitFade_Head()
    {
        transform.position = new Vector3(-570, 59.7f, 126.5f);  // ���� ���������� ������ �̵�
    }
}
