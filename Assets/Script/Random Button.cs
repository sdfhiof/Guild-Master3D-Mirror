using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomButton : MonoBehaviour
{
    public Button unit1B, unit2B, unit3B, unit4B;

    void Start()
    {
        unit1B.onClick.AddListener(RandomItem);
        //unit1B.onClick.RemoveAllListeners();

        unit2B.onClick.AddListener(RandomItem);
        //unit2B.onClick.RemoveAllListeners();
        
        unit3B.onClick.AddListener(RandomItem);
        //unit3B.onClick.RemoveAllListeners();
        
        unit4B.onClick.AddListener(RandomItem);
        //unit4B.onClick.RemoveAllListeners();
    }
    void RandomItem()
    {
        int randomI = Random.Range(0, 5) + 1;

        switch (randomI)
        {
            case 1:
                Debug.Log("랜덤 아이템 1 적용");
                break;
            case 2:
                Debug.Log("랜덤 아이템 2 적용");
                break;
            case 3:
                Debug.Log("랜덤 아이템 3 적용");
                break;
            case 4:
                Debug.Log("랜덤 아이템 4 적용");
                break;
            case 5:
                Debug.Log("랜덤 아이템 5 적용");
                break;
        }
    }
}
