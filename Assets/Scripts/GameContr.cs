using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContr : MonoBehaviour
{
    public static GameContr Instance;

    [SerializeField] private bool[] WinCells = new bool[9];
    [SerializeField] private bool[] FilledCells = new bool[9];
    private bool[] bufCells = new bool[9];

    [SerializeField] private GameObject CellsGameObject;
    [SerializeField] private GameObject SPocketGameObject;
    [SerializeField] private GameObject BPocketGameObject;
    [SerializeField] private GameObject sysMenuGameObject;
    [SerializeField] private GameObject DragObj;
    [Space(5)]
    [SerializeField] private TextMeshProUGUI win_lose_text;
    [Space(5)]
    [SerializeField] private GameObject BlockPrefab;
    [Space(5)]
    public bool BlockInSmallPocket;
    public bool BlockInBigPocket;

    public void RefreshLevel()
    {
        ClearAllCells();
        for (int i = 0; i < FilledCells.Length; i++)
            bufCells[i] = FilledCells[i];
        FillCellsOnStart();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < FilledCells.Length; i++)
            bufCells[i] = FilledCells[i];
        FillCellsOnStart();
    }
    private bool AmIRight(int num) // Возвращает состояние true если выбрана правильная клетка
    {
        return WinCells[num];
    }

    public bool CanIFill(int num) // Возвращает состояние клетки (если заколнена = false)
    {
        return !FilledCells[num];
    }

    public void TryFill(int num)
    {
        if (CanIFill(num) == true)
        {
            bufCells[num] = true;
            ClearAllCells();
            FillCell();

            if (AmIRight(num))
            {
                win_lose_text.text = "ПОБЕДА!";
                sysMenuGameObject.SetActive(true);
            }
            else
            {
                win_lose_text.text = "ОШИБКА!";
                sysMenuGameObject.SetActive(true);
            }
        }
    }
    private void FillCellsOnStart()
    {
        for (int i = 0; i < FilledCells.Length; i++)
        {
            if (FilledCells[i])
            {
                var Block = Instantiate(BlockPrefab, CellsGameObject.transform.GetChild(i));
                Block.GetComponent<ItemDrag>().Init(DragObj.transform, SPocketGameObject.transform, BPocketGameObject.transform, CellsGameObject.transform, false);
            }

        }
        if (BlockInBigPocket)
        {
            var Block = Instantiate(BlockPrefab, BPocketGameObject.transform);
            Block.GetComponent<ItemDrag>().Init(DragObj.transform, SPocketGameObject.transform, BPocketGameObject.transform, CellsGameObject.transform);
        }

        if (BlockInSmallPocket)
        {
            var Block = Instantiate(BlockPrefab, SPocketGameObject.transform);
            Block.GetComponent<ItemDrag>().Init(DragObj.transform, SPocketGameObject.transform, BPocketGameObject.transform, CellsGameObject.transform);
        }

    }
    private void FillCell()
    {
        for (int i = 0; i < bufCells.Length; i++)
        {
            if (bufCells[i])
            {
                var Block = Instantiate(BlockPrefab, CellsGameObject.transform.GetChild(i));
                Block.GetComponent<ItemDrag>().Init(DragObj.transform, SPocketGameObject.transform, BPocketGameObject.transform, CellsGameObject.transform, false);
            }
        }

    }
    private void RefreshCellsState() // Обновляет массив сетки с учетом заполненности плиток в иерархии
    {
        for (int i = 0; i < CellsGameObject.transform.childCount; i++)
        {
            if (CellsGameObject.transform.GetChild(i).transform.childCount >= 1)
            {
                FilledCells[i] = true;
            }
        }
    }

    private void ClearAllCells()
    {
        for (int i = 0; i < CellsGameObject.transform.childCount; i++)
        {
            if (CellsGameObject.transform.GetChild(i).childCount > 0)
                CellsGameObject.transform.GetChild(i).GetChild(0).GetComponent<ItemDrag>().NeedToDestroy();

            
        }
    }
}
