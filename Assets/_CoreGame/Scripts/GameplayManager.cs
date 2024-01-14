using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private Dictionary<Cell, Color> solution = new Dictionary<Cell, Color>();
    public Dictionary<Cell, Color> Solution { get => solution; set => solution = value; }
    public Transform imageHolder;
    private int count;
    private int moves;

    public Action<int> OnMoveValueChange;
    public Action<int> OnCountValueChange;
    public int Count
    {
        get => count;
        set
        {
            count = value;
            OnCountValueChange?.Invoke(count);
        }
    }

    public int Moves
    {
        get => moves;
        set
        {
            moves = value;
            OnMoveValueChange?.Invoke(moves);
        }
    }

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void Generate(bool isRandomColor, int xSize, int ySize, int generatorMode, int offset)
    {
        GridManager.Instance.CreateGameplay(isRandomColor, xSize, ySize, generatorMode, offset);
    }
}
