using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private int x, y;
    private bool isFixed;
    [SerializeField] private Image image;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool IsFixed { get => isFixed; set => isFixed = value; }
    public Image Image
    {
        get => image;
        set
        {
            Image = value;
            Image.transform.SetParent(transform);
            Image.transform.localPosition = Vector3.zero;
        }
    }
}
