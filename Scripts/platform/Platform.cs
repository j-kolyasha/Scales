using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public event UnityAction ItemUpdate;

    [SerializeField, Range(0f, 1f)] private float _speed;
    [SerializeField] private float _maxHightPosition;
    [SerializeField] private float _minHightPosition;
    [SerializeField] private Image _indicator;

    private Vector3 _currentPosition;
    private List<Item> _items;

    public Vector3 Position => _currentPosition;

    public void ColorUpdate(Color color)
    {
        _indicator.color = color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item))
        {
            _items.Add(other.GetComponent<Item>());
            StartCoroutine(nameof(NewItem));
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            _items.Remove(other.GetComponent<Item>());
            StartCoroutine(nameof(DeleteItem));
        }
    }
    private IEnumerator NewItem()
    {
        float y = CalculatePosition();

        while(transform.position.y >= _maxHightPosition - y)
        {
            Vector3 position = transform.position;
            position.y -= _speed;
            transform.position = position;
            yield return new WaitForEndOfFrame();
        }

        PositionUpdate();
        ItemUpdate?.Invoke();
    }
    private IEnumerator DeleteItem()
    {
        float y = CalculatePosition();

        while (transform.position.y <= _maxHightPosition - y)
        {
            Vector3 position = transform.position;
            position.y += _speed;
            transform.position = position;
            yield return new WaitForEndOfFrame();
        }

        PositionUpdate();
        ItemUpdate?.Invoke();
    }
    private float CalculatePosition()
    {
        float Weight = 0;
        foreach (Item item in _items)
            Weight += item.Weight;

        float percent = (100 * Weight) / GameManager.Instance.ConfigurationLevel.WeightForOneScale;
        float y = (((_maxHightPosition - _minHightPosition) / 2) * percent) / 100;

        return y;
    }
        
    private void PositionUpdate()
    {
        Vector3 position = new Vector3(0, transform.position.y, 0);
        _currentPosition = position;
    }
    private void Start()
    {
        _items = new List<Item>();

        PositionUpdate();
    }
}