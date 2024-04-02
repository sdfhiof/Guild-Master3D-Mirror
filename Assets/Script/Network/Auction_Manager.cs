using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class Auction_Manager : NetworkBehaviour
{

    static Auction_Manager Instance;
    public List<GameObject> _guildUnitList = new List<GameObject>();
    //public List<GameObject> _playerList = new List<GameObject>();

    public List<PlayerInfo> _playerList = new List<PlayerInfo>();

    public List<TMP_Text> _bidList = new List<TMP_Text>();

    public TMP_InputField BidInputTMP;

    public List<GameObject> _unBidedUnitList = new List<GameObject>();
    public static Auction_Manager GetInstance() { Init(); return Instance; }

    uint playerNum = 0;
    uint lastPlayerIndex = 0;
    uint timeOfAuction = 0;

    void Start()
    {

    }

    void Update()
    {
        bidPlayer();
    }

    static void Init()
    {
        GameObject AM = GameObject.Find("AuctionManager");
        Instance = AM.GetComponent<Auction_Manager>();
    }

    public void addPlayer(PlayerInfo testPlayer)
    {
        _playerList.Add(testPlayer);
        Debug.Log(_playerList.Count);
    }

    public void bidPlayer()
    {
        for(int i = 0; i < _playerList.Count; i++)
        {
            _bidList[i].text = _playerList[i].BidPrice.ToString();
        }
    }

    public void OnBid()
    {
        Debug.Log("On Bid");
        int _bidPrice = 0;

        _bidPrice = int.Parse(BidInputTMP.text.ToString());
        switch (_bidPrice <= 0 || PlayerInfo.LocalPlayer.nowMoney < _bidPrice)
        {
            case true:
                Debug.Log("wrong bid");
                break;
            case false:
                Debug.Log("right bid");
                PlayerInfo.LocalPlayer.BidPrice = _bidPrice;
                break;
        }
    }

    void DebugLogUnit()
    {
        foreach (GameObject i in _guildUnitList)
        {
            UnityEngine.Debug.Log(i.name);
        }
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }

    void whoIsLast()
    {
        lastPlayerIndex = playerNum - 1;
    }

    void auction()
    {

    }
}
