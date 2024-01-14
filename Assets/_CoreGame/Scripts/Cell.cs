using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Image xImage;

    private int x, y;
    private bool isFixed;
    private static Cell dragCell;
    private static Cell dropCell;
    public bool IsCorrect;
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool IsFixed
    {
        get => isFixed;
        set
        {
            isFixed = value;
            xImage.enabled = value;
        }
    }
    public Image Image
    {
        get => image;
        set
        {
            image = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsFixed) return;
        dragCell = this;
        Image.transform.SetParent(GridManager.Instance.temp);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsFixed) return;
        Image.transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragCell == null) return;
        dropCell = this;
        if (dropCell.IsFixed)
        {
            dropCell = dragCell;
        }
        SwapImage();
        if (dropCell == dragCell) return;
        CheckSoluion(dragCell);
        CheckSoluion(dropCell);
        Debug.Log(GridManager.Instance.count);
        if (GridManager.Instance.count == 0)
        {
            Debug.Log("Victory");
        }
        dragCell = dropCell = null;
    }
    private void SwapImage()
    {
        Image temp = dragCell.Image;
        dragCell.Image = dropCell.Image;
        dropCell.Image = temp;
        dragCell.Image.transform.SetParent(dragCell.transform);
        dragCell.Image.transform.localPosition = Vector2.zero;
        dropCell.Image.transform.SetParent(dropCell.transform);
        dropCell.Image.transform.localPosition = Vector2.zero;
    }
    private void CheckSoluion(Cell temp)
    {
        if (temp.Image.color == GridManager.Instance.Solution[temp])
        {
            temp.IsCorrect = true;
            GridManager.Instance.count--;
        }
        else if (temp.IsCorrect)
        {
            GridManager.Instance.count++;
            temp.IsCorrect = false;
        }

    }

}
