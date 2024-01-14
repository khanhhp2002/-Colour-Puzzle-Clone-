using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Color[] colors = new Color[4];
    [SerializeField] private int xSize, ySize;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Image gridPrefab;
    void Start()
    {
        GridSetting();
        GridGenerator();
    }

    private void GridSetting()
    {
        float width = (gridLayoutGroup.transform as RectTransform).rect.width;
        float cellSize = width / xSize;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.constraintCount = xSize;
    }

    private void GridGenerator()
    {
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Image gridImage = Instantiate(gridPrefab, transform);
                Color temp1 = Color.Lerp(colors[0], colors[1], j / (float)(xSize - 1));
                Color temp2 = Color.Lerp(colors[2], colors[3], j / (float)(xSize - 1));
                gridImage.color = Color.Lerp(temp1, temp2, i / (float)(ySize - 1));
            }
        }
    }

    private void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

}
