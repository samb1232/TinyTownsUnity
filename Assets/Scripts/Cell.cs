using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool isFree;

    private Item takenItem = Item.None;

    private void Start()
    {
        isFree = true;
    }

    internal void MarkTaken(Item item)
    {
        takenItem = item;
        isFree = false;
    }

    internal Item GetItem() { return takenItem; }

    internal bool CheckIsFree() { return isFree; }
}
