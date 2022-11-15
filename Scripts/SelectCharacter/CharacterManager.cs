using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Character  // ���������� ĳ���� �̸� ����(0���� �ڵ����� ���� �Ű���)
    {
        Boy, Girl, Princess
    }


public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    // ĳ������ �ε����� ������ ��ųʸ� ����
    public Dictionary<Character, int> selected = new Dictionary<Character, int>(); 
    
    // ���õ� ĳ������ �ε����� ������ ����
    public int selectedCharIndex; 


    private void Awake()
    {
        if (instance == null) instance = this;  //instance ���� �ʱ�ȭ �Ǿ��ִ��� üũ
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);  // �� ��ȯ �Ŀ��� ������Ʈ �ı� x
    }

    public Character select_character;  // ������ ĳ���� �� ���� (ĳ���� �����ϸ� �ٸ� �����ε� ����)

    void Start()
    {
        // ĳ���͸��� �ε��� ��ȣ �ο�
        selected[Character.Boy] = 0;
        selected[Character.Girl] = 1;
        selected[Character.Princess] = 2;
    }

    void Update()
    {
        // ������ ���� �ε��� ��ȣ�� ������ ����
        selectedCharIndex = selected[select_character]; 
    }

}
