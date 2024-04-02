using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // camera�� ���󰡾� �� target ����
    public GameObject Player;
    // ���� ��ǥ�� ��ġ �������� public ������ ����
    public Vector3 offset;
    public Transform targetPlayer;

    void setPlayer()
    {
        Player = GameObject.FindWithTag("Player");
        targetPlayer = Player.transform;
        transform.position = targetPlayer.position + offset;
    }

    void Update()
    {
        setPlayer();
    }
}
