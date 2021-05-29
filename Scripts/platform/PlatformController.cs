using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformContainer[] _platforms;

    private Item[] _allItems;

    private void CalculateDistance()
    {
        for (int i = 0; i < _platforms.Length; i++)
        {
            Platform platform1 = _platforms[i].Platforms[0];
            Platform platform2 = _platforms[i].Platforms[1];

            float distance = Vector3.Distance(platform1.Position, platform2.Position);
            if (distance < .1f)
            {
                platform1.ColorUpdate(Color.green);
                platform2.ColorUpdate(Color.green);

                _platforms[i].Equalized = true;
                CheckEndGame();
            }
            else
            {
                _platforms[i].Equalized = false;

                if (distance < 2f)
                {
                    platform1.ColorUpdate(Color.yellow);
                    platform2.ColorUpdate(Color.yellow);
                }
                else
                {
                    platform1.ColorUpdate(Color.red);
                    platform2.ColorUpdate(Color.red);
                }
            }
        }
    }
    private void CheckEndGame()
    {
        if (_platforms.Where(platform => platform.Equalized == true).Count() == _platforms.Length &&
            _allItems.Where(item => item.ThePlatform == true).Count() == _allItems.Length)
            GameManager.Instance.Victory();
    }
    private void Init()
    {
        _allItems = GameObject.FindObjectsOfType<Item>();
    }
    private void Start()
    {
        for(int i = 0; i < _platforms.Length; i++)
        {
            _platforms[i].Platforms[0].ItemUpdate += CalculateDistance;
            _platforms[i].Platforms[1].ItemUpdate += CalculateDistance;
        }

        Invoke(nameof(Init), 1f);
    }
    private void OnDisable()
    {
        for (int i = 0; i < _platforms.Length; i++)
        {
            _platforms[i].Platforms[0].ItemUpdate -= CalculateDistance;
            _platforms[i].Platforms[1].ItemUpdate -= CalculateDistance;
        }
    }
}

[Serializable]
class PlatformContainer
{
    [HideInInspector] public bool Equalized = false;
    public Platform[] Platforms;    
}