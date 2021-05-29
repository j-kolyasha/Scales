using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;
    [SerializeField] private Transform _spawnPosition;

    private void SpawnItem()
    {
        Item[] items = GenerateItem();
    
        for (int i = 0; i < items.Length; i++)
        {
            Instantiate(items[i], _spawnPosition);
        }
    }
    

    private Item[] GenerateItem()
    {
        List<Item> items = new List<Item>();

        List<float> weights = new List<float>();
        for (int i = 0; i < _itemPrefabs.Length; i++)
            weights.Add(_itemPrefabs[i].Weight);

        float allWeight = 0;
        while (allWeight < GameManager.Instance.ConfigurationLevel.RequiredAllWeight)
        {
            int value = Random.Range(0, (int)weights.Sum());
            float sum = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                sum += weights[i];
                Item item = _itemPrefabs[i];
                if (sum >= value && item.Weight <= GameManager.Instance.ConfigurationLevel.RequiredAllWeight - allWeight)
                {
                    allWeight += item.Weight;
                    items.Add(item);
                }
            }
        }

        return items.ToArray();
    }

    private void Start()
    {
        SpawnItem();
    }
}