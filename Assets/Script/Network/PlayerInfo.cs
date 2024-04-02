using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{
    public TMP_Text nameDisplayTMP; 
    public static PlayerInfo LocalPlayer;
    public MeshRenderer MeshRenderer;

    [Networked, OnChangedRender(nameof(ColorChanged))]
    public Color NetworkedColor { get; set; }

    [Networked]
    public NetworkString<_64> PlayerName { get; set; } 

    public int nowMoney = 3000;
    public int BidPrice;

    void Start()
    {
        
    }

    void Update()
    {
        changeColor();
    }

    public void changeColor()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            Debug.Log("Color Changed");
        }
    }

    //public void OnBid()
    //{
    //    Debug.Log("On Bid");
    //    int _bidPrice = 0;

    //    //int.TryParse(BidInputTMP.text.ToString(), out _bidPrice);

    //    //_bidPrice = int.Parse(BidInputTMP.text.ToString());
    //    switch (_bidPrice <= 0 || LocalPlayer.nowMoney < _bidPrice)
    //    {
    //        case true:
    //            Debug.Log("wrong bid");
    //            break;
    //        case false:
    //            BidPrice = _bidPrice;
    //            break;
    //    }
    //}

    public override void Spawned()
    {
        LocalPlayer = this;
        if(HasInputAuthority)
        {
            PlayerName = Runner.GetComponent<PlayerSpawner>().playerName;
        }
        nameDisplayTMP.text = PlayerName.ToString();
        transform.gameObject.name = PlayerName.ToString();
    }

    void ColorChanged()
    {
        MeshRenderer.material.color = NetworkedColor;
    }
}
