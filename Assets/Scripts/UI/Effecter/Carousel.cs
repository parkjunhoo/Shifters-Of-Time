using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{


    RectTransform rootRect;
    RectTransform rect;
    CanvasScaler canvasScaler;
    GridLayoutGroup grid;

    [SerializeField]
    ScrollRect ScrollRect;

    float scrollRate;
    float scrollSens;

    int _index = 0;
    int _childCount;
    public int Index { get { return _index; } set { _index = value; if(OnIndexChanged!=null) OnIndexChanged.Invoke(); } }

    Vector2 DynamicCellSize;
    Vector2 DynamicSpacing;

    float magnetRange = 3f;
    float MoniotoringRange = 500f;

    bool isMove = false;

    public Action OnIndexChanged = null;


    void Start()
    {
        rootRect = transform.root.GetComponent<RectTransform>();
        rect = gameObject.GetComponent<RectTransform>();
        canvasScaler = transform.root.GetComponent<CanvasScaler>();
        grid = gameObject.GetComponent<GridLayoutGroup>();

        float ratioX = rootRect.rect.width / canvasScaler.referenceResolution.x;
        float ratioY = rootRect.rect.height / canvasScaler.referenceResolution.y;


        DynamicCellSize = new Vector2(grid.cellSize.x * ratioX, grid.cellSize.y * ratioY);
        DynamicSpacing = new Vector2(grid.spacing.x * ratioX, grid.spacing.y * ratioY);
        grid.cellSize = DynamicCellSize;
        grid.spacing = DynamicSpacing;

        _childCount = transform.childCount;
        scrollRate = ScrollRect.decelerationRate;
        scrollSens = ScrollRect.scrollSensitivity;
        StartCoroutine(Monitoring());
    }



    IEnumerator Monitoring()
    {
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < _childCount; i++)
            {
                if (i == _index) continue;
                float destPos = DynamicCellSize.x * (float)-i;
                if (rect.anchoredPosition.x > (destPos - MoniotoringRange) && rect.anchoredPosition.x < (destPos + MoniotoringRange))
                    if (!isMove) CarouselMoveTo(i);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CarouselMoveTo(int i)
    {
        Index = i;
        StartCoroutine(Moving(i));
    }
    IEnumerator Moving(int i)
    {
        isMove = true;
        ScrollRect.decelerationRate = 0;
        ScrollRect.scrollSensitivity = 0;
        float destPos = DynamicCellSize.x * (float)-i;
        while (rect.anchoredPosition.x != destPos)
        {
            float destX = (destPos - rect.anchoredPosition.x) * Time.fixedDeltaTime * 10f;
            rect.anchoredPosition += new Vector2(destX, rect.anchoredPosition.y);
            if (rect.anchoredPosition.x > (destPos - magnetRange) && rect.anchoredPosition.x < (destPos + magnetRange)) break;
            yield return new WaitForSeconds(0.0001f);
        }
        isMove = false;
        ScrollRect.decelerationRate = scrollRate;
        ScrollRect.scrollSensitivity = scrollSens;
    }
}
