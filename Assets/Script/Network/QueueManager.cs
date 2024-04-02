using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class QueueManager : MonoBehaviour
{
    public string LocalPlayerName { get; set; }

    public string LocalRoomName { get; set; }

    public static QueueManager Instance { get; private set; }

    private NetworkRunner _networkRunnerPrefab;

    public TextMeshProUGUI roomName;

    public GameObject errorMessageObject;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public async void StartGame(bool joinRandomRoom)
    {
        StartGameArgs startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = joinRandomRoom ? string.Empty : LocalRoomName,
            PlayerCount = 20,
        };

        NetworkRunner newRunner = Instantiate(_networkRunnerPrefab);

        StartGameResult result = await newRunner.StartGame(startGameArgs);

        if (result.Ok)
        {
            roomName.text = "Room:  " + newRunner.SessionInfo.Name;

            GoToGame();
        }
        else
        {
            roomName.text = string.Empty;

            GoToMainMenu();

            errorMessageObject.SetActive(true);
            TextMeshProUGUI gui = errorMessageObject.GetComponentInChildren<TextMeshProUGUI>();
            if (gui)
                gui.text = result.ErrorMessage;

            Debug.LogError(result.ErrorMessage);
        }

    }

    public void GoToMainMenu()
    {
    }

    public void GoToGame()
    {

    }

    public void StartGame()
    {
        NetworkRunner runner = null;
        // If no runner has been assigned, we cannot start the game
        if (NetworkRunner.Instances.Count > 0)
        {
            runner = NetworkRunner.Instances[0];
        }

        if (runner == null)
        {
            Debug.Log("No runner found.");
            return;
        }

        //if (runner.IsSharedModeMasterClient && !TriviaManager.TriviaManagerPresent)
        //{
        //    runner.Spawn(triviaGamePrefab);
        //    showGameButton.SetActive(false);
        //}
    }
}
