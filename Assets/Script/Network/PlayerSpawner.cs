using Fusion;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject UiPrefeb;
    public GameObject ManagerPrefab;
    public string playerName = null;
    public GameObject uiJoinCanvas;
    public GameObject uiAuctionCanvas;
    public TMP_InputField nameInputField;

    public Auction_Manager AM;


    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer )
        {
            var resultingPlayer = Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
            var localPlayer = resultingPlayer.GetComponent<PlayerInfo>();

            AM.addPlayer(localPlayer);
        }

        //if (player == Runner.LocalPlayer)
        //{
        //    var resultingPlayer = Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity);

        //    FusionConnector connector = GameObject.FindObjectOfType<FusionConnector>();
        //    if (connector != null)
        //    {
        //        var testPlayer = resultingPlayer.GetComponent<TriviaPlayer>();

        //        string playerName = connector.LocalPlayerName;

        //        if (string.IsNullOrEmpty(playerName))
        //            testPlayer.PlayerName = "Player " + resultingPlayer.StateAuthority.PlayerId;
        //        else
        //            testPlayer.PlayerName = playerName;

        //        // Assigns a random avatar
        //        testPlayer.ChosenAvatar = Random.Range(0, testPlayer.avatarSprites.Length);
        //    }
        //}
    }

    //public void Playerspawn()

    public void SaveNameHideCanvas()
    {
        uiJoinCanvas.SetActive(false);
        uiAuctionCanvas.SetActive(true);
        playerName = nameInputField.text;        
    }

    

    

    //public void PlayerLeft(PlayerRef player)
    //{
    //    if (PlayerInfo.LocalPlayer != null)
    //        PlayerInfo.LocalPlayer.IsMasterClient = Runner.IsSharedModeMasterClient;
    //}
} 