using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask m_LayerMask;

    [SerializeField] private Item m_ItemType;

    private float hoverTopPosY = 5f;
    private int hoverSpeed = 5;


    private bool isDraggable;

    private bool isPickedUp;

    private Vector3 prevPosition;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isDraggable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PickUp();
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Drop();
    }

    private void PickUp()
    {
        if (!isDraggable) return;
        isPickedUp = true;
        rb.useGravity = false;
        prevPosition = transform.position;
    }

    private void Drop()
    {
        if (!isDraggable) return;
        isPickedUp = false;
        rb.useGravity = true;

        SnapOrReset();
    }

    private Ray ray;
    private RaycastHit raycastHit;

    bool isAllowedToDrop;
    private void SnapOrReset()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, m_LayerMask))
        {
            GameObject col = raycastHit.collider.gameObject;
            if (col.CompareTag("Cell") && GameManager.instance.ProcessCell(m_ItemType, col.gameObject.GetComponent<Cell>(), gameObject))
            {
                Vector3 newPos = new Vector3(
                    col.transform.position.x, 
                    transform.position.y,
                    col.transform.position.z
                    );

                transform.position = newPos;
                isDraggable = false;
            } 
            else
            {
                if (isDraggable)
                {
                    transform.position = prevPosition;
                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (isPickedUp)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, float.MaxValue, m_LayerMask))
            {
                Vector3 newPosition = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
                
                newPosition.y = (newPosition.y < hoverTopPosY) ? (newPosition.y + hoverSpeed / 10f) : hoverTopPosY;
                
                transform.position = newPosition;
                rb.velocity = Vector3.zero; rb.angularVelocity = Vector3.zero;
                transform.rotation = Quaternion.identity;

            }

        
        }
        
    }

}
