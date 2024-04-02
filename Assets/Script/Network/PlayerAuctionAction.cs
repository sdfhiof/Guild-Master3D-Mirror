using Fusion;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerAuctionAction : NetworkBehaviour
{

    //[Networked, OnChangedRender(nameof(OnBid))]
    //public uint BidPrice { get; set; }

    public int BidPrice;

    private ChangeDetector _changeDetector;

    

    
}
