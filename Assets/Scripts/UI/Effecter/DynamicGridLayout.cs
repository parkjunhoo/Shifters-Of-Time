using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{

    //���̵���Ʈ => �����̴����鶧 ��ũ�Ѻ��� �������� cellsize.x��ŭ �̵��Ҷ����� �׸��尡 �����ο�.
    void Start()
    {
        RectTransform rootRect;
        CanvasScaler canvasScaler;
        GridLayoutGroup grid;


        rootRect = transform.root.GetComponent<RectTransform>();
        canvasScaler = transform.root.GetComponent<CanvasScaler>();
        grid = gameObject.GetComponent<GridLayoutGroup>();

        float ratioX = rootRect.rect.width / canvasScaler.referenceResolution.x;
        float ratioY = rootRect.rect.height / canvasScaler.referenceResolution.y;


        Vector2 DynamicCellSize = new Vector2(grid.cellSize.x  * ratioX, grid.cellSize.y * ratioY);
        Vector2 DynamicSpacing = new Vector2(grid.spacing.x * ratioX, grid.spacing.y * ratioY);
        grid.cellSize = DynamicCellSize;
        grid.spacing = DynamicSpacing;
    }
}
