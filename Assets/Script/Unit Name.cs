using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UnitName : MonoBehaviour
{
    public List<Text> unitName = new List<Text>();
    WarManager wm;

    //public Text unit1N, unit2N, unit3N, unit4N;
    //public GameObject unit1, unit2, unit3, unit4;

    void Start()
    {
        for(int i=0; i<wm.guild.Count; i++)
        {
            unitName[i].text = wm.guild[i].name;
            Debug.Log(wm.guild[i].name);
        }

        //unit1N.text = unit1.name;
        //unit2N.text = unit2.name;
        //unit3N.text = unit3.name;
        //unit4N.text = unit4.name;
    }
}
