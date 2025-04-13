using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItemComponent : MonoBehaviour
{
    [Header("Transform")] [SerializeField] private Transform _target;
    
    [Header("Items")]
    [SerializeField] private List<DropItem> dropItems = new List<DropItem>();

    public void SpawnDropItem()
    {
        int _allChance = 0;
        dropItems.ForEach((item) =>
        {
            _allChance += item.Chanse;
        });
        int random = Random.Range(0, _allChance);
        int previousChance = 0;
        for (int i = 0; i < dropItems.Count; i++)
        {
            previousChance += dropItems[i].Chanse;
            if (previousChance >= random)
            {
                GameObject obj = Instantiate(dropItems[i].Prefab, _target.position, Quaternion.identity);
                obj.transform.SetParent(null);
                break;
            }
        }
    }
    
    [Serializable]
    private class DropItem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _chanse;
        
        public int Chanse => _chanse;
        public GameObject Prefab => _prefab;
    }
}
