using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager_ : Singleton<GameManager_>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    // Keep track of the game state
    // generate other persistent systems
    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;

    GameState currentGameState = GameState.PREGAME;
    private List<GameObject> instancedSystemPrefabs;
    List<AsyncOperation> loadOperations;

    private string currentLevelName = string.Empty;

    public GameState CurrentGameState { get { return currentGameState; } private set { currentGameState = value; } }

    void Start()
    {
        DontDestroyOnLoad(this);

        instancedSystemPrefabs = new List<GameObject>();

        // create a list of loading operations.
        loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void Update()
    {
        if (currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        { 
            UnloadLevel(currentLevelName);
        }
    }

    #region Level Loading and Unloading

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        // Makes sure that the operation is in the list so that no one is calling
        // an operation that they shouldnt be able to.
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            // dispatch messages
            // transition between scenes

            if (loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }

        }

        Debug.Log("Load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName)
    {
        // Prevents blocking when level loading
        // LoadSceneMode prevents the boot scene from being removed automatically when loaded
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
        }
        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);

        currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level" + levelName);
        }
        ao.completed += OnUnloadOperationComplete;
    }

    #endregion

    void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }

        // dispatch messages
        // transition between scenes

        OnGameStateChanged.Invoke(currentGameState, previousGameState);

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }

        instancedSystemPrefabs.Clear();

    }

    public void StartGame()
    {
        // Load initial scene
        LoadLevel("SampleScene");
    }

    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        // Implement features for quiting such as saving, etc..)
        Application.Quit();
    }
}
