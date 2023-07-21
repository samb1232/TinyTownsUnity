using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnPlate;

    [Header("Block prefabs")]
    [SerializeField] private GameObject m_WoodBlockPrefab;
    [SerializeField] private GameObject m_CornBlockPrefab;
    [SerializeField] private GameObject m_BrickBlockPrefab;
    [SerializeField] private GameObject m_GlassBlockPrefab;
    [SerializeField] private GameObject m_RockBlockPrefab;


    [Header("Cells")]
    [SerializeField] private Cell Cell1_1;
    [SerializeField] private Cell Cell1_2;
    [SerializeField] private Cell Cell1_3;
    [SerializeField] private Cell Cell1_4;
    [SerializeField] private Cell Cell2_1;
    [SerializeField] private Cell Cell2_2;
    [SerializeField] private Cell Cell2_3;
    [SerializeField] private Cell Cell2_4;
    [SerializeField] private Cell Cell3_1;
    [SerializeField] private Cell Cell3_2;
    [SerializeField] private Cell Cell3_3;
    [SerializeField] private Cell Cell3_4;
    [SerializeField] private Cell Cell4_1;
    [SerializeField] private Cell Cell4_2;
    [SerializeField] private Cell Cell4_3;
    [SerializeField] private Cell Cell4_4;

    private GameObject[] blockPrefabs;

    private Cell[,] cellMatrix;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        blockPrefabs = new GameObject[] { m_WoodBlockPrefab, m_CornBlockPrefab, m_BrickBlockPrefab, m_GlassBlockPrefab, m_RockBlockPrefab };
        SpawnBlock(blockPrefabs[Random.Range(0, 4)]);

        FillCellMatrix();
    }

    private void FillCellMatrix()
    {
        cellMatrix = new Cell[4, 4] {
            { Cell1_1, Cell1_2, Cell1_3, Cell1_4 },
            { Cell2_1, Cell2_2, Cell2_3, Cell2_4 },
            { Cell3_1, Cell3_2, Cell3_3, Cell3_4 },
            { Cell4_1, Cell4_2, Cell4_3, Cell4_4 },
        };

    }




    private void SpawnBlock(GameObject blockPrefab)
    {
        Vector3 pos = m_SpawnPlate.transform.position;
        pos.y += 2f;
        Instantiate(blockPrefab, pos, Quaternion.identity);
    }

    //Check if cell is capable to handle draggable item and mark it if it is
    internal bool ProcessCell(Item item, Cell cell, GameObject block4TEST)
    {
        if (cell.CheckIsFree())
        {
            cell.MarkTaken(item);
            cell.gameObject.GetComponent<Renderer>().material = block4TEST.GetComponent<Renderer>().material;
            return true;
        }
        else
        {
            return false;
        }
    }
}
