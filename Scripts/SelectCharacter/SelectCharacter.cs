// ���� �ۼ� ��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectCharacter : MonoBehaviour
{
    public Character character;
    Animator anim;
    public SelectCharacter[] chars;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (CharacterManager.instance.select_character == character) { OnSelect(); }  //���� �ʱ�ȭ
        else { OnDeSelect(); }
    }

    public void OnMouseUpAsButton()  //GUI Element or Collider ������ ���콺 �����ٰ� ���� ���� ȣ���
    {
        CharacterManager.instance.select_character = character;  // CharacterManager�� �ִ� select_character�� character�� �ʱ�ȭ
        OnSelect();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != this) { chars[i].OnDeSelect(); }  //������ ĳ���� �ƴϸ� false ó��
        }
    }

    void OnSelect()
    {

        anim.SetBool("boyIsDance", true);
        anim.SetBool("girlIsDance", true);
        anim.SetBool("princessIsDance", true);  //�ִϸ��̼� ��ȯ �Ķ����

    }
    void OnDeSelect()
    {
        anim.SetBool("boyIsDance", false);
        anim.SetBool("girlIsDance", false);
        anim.SetBool("princessIsDance", false);
    }
}
