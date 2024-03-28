using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Image xImage;
    private Action<Cell> returnAction;
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
        if (IsFixed || dragCell != null || dropCell != null) return;
        Debug.Log("OnBeginDrag");
        dragCell = this;
        Image.transform.SetParent(GameplayManager.Instance.ImageHolder);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsFixed || dragCell != this) return;

        dragCell.Image.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragCell == null) return;
        if (dropCell == null)
        {
            Debug.Log("OnEndDrag");
            MoveDragCellBack();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragCell == null || dropCell != null) return;

        dropCell = this;
        if (dragCell == this || dropCell.IsFixed)
        {
            MoveDragCellBack();
            return;
        }
        Debug.Log("OnDrop");
        SwapImage();

        if (dropCell == dragCell) return;
        GameplayManager.Instance.Moves++;
        CheckSoluion(dragCell);
        CheckSoluion(dropCell);

        if (GameplayManager.Instance.Count == 0)
        {
            Debug.Log("Victory");
        }
    }

    private void SwapImage()
    {
        Image temp = dragCell.Image;
        dragCell.Image = dropCell.Image;
        dropCell.Image = temp;

        dragCell.Image.transform.SetParent(GameplayManager.Instance.ImageHolder);

        dragCell.Image.transform.DOMove(dragCell.transform.position, 0.2f).onComplete += () =>
        {
            dragCell.Image.transform.SetParent(dragCell.transform);
            dragCell = null;
        };

        dropCell.Image.transform.DOMove(dropCell.transform.position, 0.2f).onComplete += () =>
        {
            dropCell.Image.transform.SetParent(dropCell.transform);
            dropCell = null;
        };
    }

    private void CheckSoluion(Cell temp)
    {
        if (temp.Image.color == GameplayManager.Instance.Solution[temp])
        {
            temp.IsCorrect = true;
            GameplayManager.Instance.Count--;
        }
        else if (temp.IsCorrect)
        {
            GameplayManager.Instance.Count++;
            temp.IsCorrect = false;
        }
    }

    public void Initialize(Action<Cell> returnAction)
    {
        this.returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        this.returnAction?.Invoke(this);
    }

    private void MoveDragCellBack()
    {
        dragCell.Image.transform.DOMove(dragCell.transform.position, 0.2f).onComplete += () =>
        {
            dragCell.Image.transform.SetParent(dragCell.transform);
            dragCell = null;
            dropCell = null;
        };
    }
}
