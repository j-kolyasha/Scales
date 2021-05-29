using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class Item : MonoBehaviour, IItem
{
    [SerializeField, Range(0f, 20f)] private float _weight;

    private Rigidbody _rigidbody;
    private TrailRenderer _trailRenderer;
    private bool _thePlatform;

    public float Weight => _weight;
    public bool ThePlatform => _thePlatform;
    public Rigidbody Rigidbody => _rigidbody;

    public string GetName()
    {
        return transform.name.Replace("(Clone)", "");
    }
    public void Drag()
    {
        _trailRenderer.enabled = true;
        _rigidbody.useGravity = false;
    }
    public void Drop()
    {
        _trailRenderer.enabled = false;
        _rigidbody.useGravity = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            _thePlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            _thePlatform = false;
        }
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();

        Drop();
    }
}