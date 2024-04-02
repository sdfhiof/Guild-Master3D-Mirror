using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WarManager : MonoBehaviour
{
    Units unit;
    public Units tUnit; // 
    public List<bool> CheckAllDie;

    public Transform unitPool;

    public List<Units> enemy = new List<Units>(); // 적 리스트
    public List<Units> guild = new List<Units>(); // 아군 리스트

    public GameObject unitPosMiniE; // 미니맵에 아군을 표시할 프리펩
    public GameObject unitPosMiniG; // 미니맵에 적을 표시할 프리펩
    public GameObject instance; // 가져온 프리펩 저장할 변수

    public void Start()
    {
        var newGameObject = new GameObject().AddComponent<SingletonPatten>();
        unit = GetComponent<Units>();

        // 리소스 폴더의 프리펩 가져오기
        unitPosMiniG = Resources.Load<GameObject>("mini_Guild"); 
        unitPosMiniE = Resources.Load<GameObject>("mini_Enemy");

        GroupingUnit();
        CheckAllDie = new List<bool>(new bool[enemy.Count]);  // 다 죽었는지 확인

        SetUnitMinimap();
    }

    void Update()
    {

    }
    public void FindTargetUnit(List<Units> list)
    {
        float minDistance = unit.GetRange(list[0]);

        for (int i = 0; i < list.Count; i++) // 각각 거리 계산해서 타겟 유닛 설정
        {
            if (minDistance >= unit.GetRange(list[i]) && !list[i].doDie || tUnit.doDie)
            {
                minDistance = unit.GetRange(list[i]);
                tUnit = list[i];
            }
        }
    }
    public void GroupingUnit() // 아군인지 적인지 구분
    {
        enemy.Clear();

        for (var i = 0; i < unitPool.childCount; i++)
        {
            if (unitPool.GetChild(i).tag == "Guild")
            {
                guild.Add(unitPool.GetChild(i).GetComponent<Units>());
            }
            else if (unitPool.GetChild(i).tag == "Enemy")
            {
                enemy.Add(unitPool.GetChild(i).GetComponent<Units>());
            }
            else
            {
                // 길드 및 적이 아닌 태그
            }
        }
    }

    void SetUnitMinimap() // 길드면 길드 프리펩을 적이면 적 프리펩을 가져오는 함수
    {
        if(unit.tag == "Guild")
        {
            instance = Instantiate(unitPosMiniG, unit.transform.position, Quaternion.identity);
        }
        if (unit.tag == "Enemy")
        {
            instance = Instantiate(unitPosMiniE, unit.transform.position, Quaternion.identity);
        }
        instance.transform.SetParent(unit.transform);
    }
}
