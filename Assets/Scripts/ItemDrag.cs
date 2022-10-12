using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Transform _dragParent;
    private Transform _origParent;
    private Transform _BpocParent;
    private Transform _SpocParent;
    private Transform _CellsParent;

    private RectTransform _rT;

    private bool _canDrag = true;

    [SerializeField] private float _maxRangeForCell;

    private void Start()
    {
        _rT = GetComponent<RectTransform>();
    }

    public void Init(Transform dragParent,Transform SpocParent,Transform BpocParent,Transform cellParent, bool canDrag)
    {
        _dragParent = dragParent;
        _origParent = transform.parent;

        _SpocParent = SpocParent;
        _BpocParent = BpocParent;
        _CellsParent = cellParent;

        _canDrag = canDrag;
    }
    public void Init(Transform dragParent, Transform SpocParent, Transform BpocParent, Transform cellParent)
    {
        _dragParent = dragParent;
        _origParent = transform.parent;

        _SpocParent = SpocParent;
        _BpocParent = BpocParent;
        _CellsParent = cellParent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            transform.SetParent(_dragParent);
            transform.localScale = Vector3.one;
        }
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragParent as RectTransform, Input.mousePosition, eventData.enterEventCamera, out pos);
            _rT.anchoredPosition = pos;
        }
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            int closestIndex = 0;
            float _distance = 0;

            for (int i = 0; i < _CellsParent.childCount; i++)
            {
                if ((Vector3.Distance(_rT.position, _CellsParent.GetChild(i).GetComponent<RectTransform>().position) <
                    Vector3.Distance(_rT.position, _CellsParent.GetChild(closestIndex).GetComponent<RectTransform>().position)) && 
                    GameContr.Instance.CanIFill(i)) 
                {
                    
                    closestIndex = i;
                    _distance = Vector3.Distance(_rT.position, _CellsParent.GetChild(i).GetComponent<RectTransform>().position);
                }


            }

            if (Vector3.Distance(_rT.position, _SpocParent.position) < _maxRangeForCell)
            {
                transform.SetParent(_SpocParent.transform);
                _rT.anchoredPosition = Vector2.zero;
                _rT.localScale = Vector3.one;
            }
            else if (Vector3.Distance(_rT.position, _BpocParent.position) < _maxRangeForCell)
            {
                transform.SetParent(_BpocParent.transform);
                _rT.anchoredPosition = Vector2.zero;
                _rT.localScale = Vector3.one;
            }
            else if (_distance < _maxRangeForCell)
            {
               if (GameContr.Instance.CanIFill(closestIndex))
                {
                    GameContr.Instance.TryFill(closestIndex);
                    Destroy(gameObject);
                }
               
            }
            else
            {
                transform.SetParent(_origParent);
                _rT.anchoredPosition = Vector2.zero;
                _rT.localScale = Vector3.one;
            }
        }
        
    }
    public void NeedToDestroy()
    {
        Destroy(gameObject);
    }

}
