using UnityEngine;
using UnityEngine.Events;

public class Iteraction : MonoBehaviour
{
    public event UnityAction<string> ItemSelection;

    [SerializeField] private LayerMask _platfromMask;
    [SerializeField] private LayerMask _iteractionMask;
    [SerializeField, Range(0f, 20f)] private float _distanceFromCamera;
    [SerializeField, Range(0f, 20f)] private float _speedModifire;

    private Vector3 _mousePosition;
    private Vector3 _velocityItem;
    private Item _targetItem;

    private void Update()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            _mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            if (_targetItem == null)
            {
                FindItem();
            }
            else
            {
                Drag();
            }
        }
        else if (_targetItem != null)
        {
            ItemSelection?.Invoke("");
            _targetItem.Drop();
            Drop();
        }
    }

    private void FindItem()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out hitInfo, 30f, _iteractionMask))
        {
            _targetItem = hitInfo.collider.GetComponent<Item>();
            ItemSelection?.Invoke(_targetItem.GetName());
            _targetItem.Drag();
        }
    }

    private void Drag()
    {
        Vector3 pointPosition = _mousePosition;
        pointPosition.z = _distanceFromCamera;
        Vector3 itemPosition = Camera.main.ScreenToWorldPoint(pointPosition);

        _velocityItem.x = Input.GetAxis("Mouse X");
        _velocityItem.y = Input.GetAxis("Mouse Y");

        _targetItem.Rigidbody.velocity = Vector3.zero;
        _targetItem.transform.position = itemPosition;
    }

    private void Drop()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        if (Physics.Raycast(ray, 30f, _platfromMask))
        {
            Vector3 currentPosition = _targetItem.transform.position;
            currentPosition.z = 8;
            _targetItem.transform.position = currentPosition;
        }

        _targetItem.Rigidbody.velocity = _velocityItem * _speedModifire;
        _targetItem = null;
    }
}