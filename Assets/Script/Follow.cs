using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // camera가 따라가야 할 target 변수
    public GameObject Player;
    // 따라갈 목표와 위치 오프셋을 public 변수로 선언
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
