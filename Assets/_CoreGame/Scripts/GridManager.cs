using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private Color[] colors = new Color[4];
    [SerializeField] private int xSize, ySize;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Mode generatorMode;
    [SerializeField, Min(1)] private int offset;
    private Dictionary<Cell, Color> solution = new Dictionary<Cell, Color>();
    public Transform temp;
    public Dictionary<Cell, Color> Solution { get => solution; set => solution = value; }
    public int count;
    void Start()
    {
        GridSetting();
        GridGenerator();
    }

    private void GridSetting()
    {
        float width = (gridLayoutGroup.transform as RectTransform).rect.width;
        float height = (gridLayoutGroup.transform as RectTransform).rect.height;
        float cellWidth = width / xSize;
        float cellHeight = height / ySize;
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
        gridLayoutGroup.constraintCount = xSize;
    }

    private void GridGenerator()
    {
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Cell cell = Instantiate(cellPrefab, this.transform);
                Color temp1 = Color.Lerp(colors[0], colors[1], j / (float)(xSize - 1));
                Color temp2 = Color.Lerp(colors[2], colors[3], j / (float)(xSize - 1));
                Color res = Color.Lerp(temp1, temp2, i / (float)(ySize - 1));
                cell.Image.color = res;
                cell.X = j;
                cell.Y = i;
                Solution.Add(cell, res);
            }
        }
        switch (generatorMode)
        {
            case Mode.FixedBorder:
                FixedBorderMode(offset);
                break;
            case Mode.FixedCenter:
                FixedCenterMode(offset);
                break;
            case Mode.FixedThreeRows:
                FixedThreeRowsMode(offset);
                break;
            case Mode.FixedThreeColumns:
                FixedThreeColumnsMode(offset);
                break;
            case Mode.FixedCross:
                FixedCrossMode(offset);
                break;
            case Mode.FixedCornerOnly:
                FixedCornerOnlyMode(offset);
                break;
            default:
                break;
        }
    }

    private void FixedBorderMode(int offset)
    {
        List<Cell> suffleCells = new List<Cell>();
        offset = Mathf.Clamp(offset, 1, Mathf.Max((Mathf.Min(xSize, ySize) / 2) - 1, 0));
        Debug.Log(offset);
        int min = offset - 1;
        int xMax = xSize - offset;
        int yMax = ySize - offset;
        foreach (Cell cell in Solution.Keys)
        {
            if (cell.X <= min || cell.X >= xMax || cell.Y <= min || cell.Y >= yMax)
                cell.IsFixed = true;
            else
            {
                cell.IsFixed = false;
                suffleCells.Add(cell);
            }
        }
        Shuffle(suffleCells);
    }

    private void FixedCenterMode(int offset)
    {
        List<Cell> suffleCells = new List<Cell>();
        offset = Mathf.Clamp(offset, 1, Mathf.Max((Mathf.Min(xSize, ySize) / 2), 0));
        Debug.Log(offset);
        int min = offset - 1;
        int xCenterMax = xSize - offset;
        int yCenterMax = ySize - offset;
        foreach (Cell cell in Solution.Keys)
        {
            if ((cell.X == 0 && cell.Y == 0) || (cell.X == xSize - 1 && cell.Y == 0) || (cell.X == 0 && cell.Y == ySize - 1) || (cell.X == xSize - 1 && cell.Y == ySize - 1))
            {
                cell.IsFixed = true;
                continue;
            }
            if (cell.X >= min && cell.X <= xCenterMax)

                if (cell.Y >= min && cell.Y <= yCenterMax)
                {
                    cell.IsFixed = true;
                    continue;
                }

            cell.IsFixed = false;
            suffleCells.Add(cell);
        }
        Shuffle(suffleCells);
    }

    private void FixedThreeRowsMode(int offset)
    {
        List<Cell> suffleCells = new List<Cell>();
        offset = Mathf.Clamp(offset, 0, Mathf.Max(Mathf.FloorToInt((ySize + 1) / 2) - 2, 0));
        Debug.Log(offset);
        int yCenterMin = Mathf.FloorToInt((ySize - 1) / 2f) - offset + 1;
        int yCenterMax = Mathf.CeilToInt((ySize - 1) / 2f) + offset - 1;
        foreach (Cell cell in Solution.Keys)
        {
            if (cell.Y == 0 || cell.Y == ySize - 1 || (cell.Y >= yCenterMin && cell.Y <= yCenterMax))
                cell.IsFixed = true;
            else
            {
                cell.IsFixed = false;
                suffleCells.Add(cell);
            }
        }
        Shuffle(suffleCells);
    }

    private void FixedThreeColumnsMode(int offset)
    {
        List<Cell> suffleCells = new List<Cell>();
        offset = Mathf.Clamp(offset, 0, Mathf.Max(Mathf.FloorToInt((xSize + 1) / 2) - 2, 0));
        Debug.Log(offset);
        int xCenterMin = Mathf.FloorToInt((xSize - 1) / 2f) - offset + 1;
        int xCenterMax = Mathf.CeilToInt((xSize - 1) / 2f) + offset - 1;
        foreach (Cell cell in Solution.Keys)
        {
            if (cell.X == 0 || cell.X == xSize - 1 || (cell.X >= xCenterMin && cell.X <= xCenterMax))
                cell.IsFixed = true;
            else
            {
                cell.IsFixed = false;
                suffleCells.Add(cell);
            }
        }
        Shuffle(suffleCells);
    }

    private void FixedCrossMode(int offset)
    {
        //pending
    }

    private void FixedCornerOnlyMode(int offset)
    {
        //pending
    }

    private void Shuffle(List<Cell> listCells)
    {
        count = listCells.Count;
        for (int i = count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i);
            Color temp = listCells[i].Image.color;
            listCells[i].Image.color = listCells[randomIndex].Image.color;
            listCells[randomIndex].Image.color = temp;
        }
    }
}
