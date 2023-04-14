using System;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy Game/" + nameof(AssetsContext), order = 0)]
public class AssetsContext : ScriptableObject
{
    [SerializeField] private GameObject[] _objects;

    public GameObject GetObjectOfType(Type targetType, string targetName = null)
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            var obj = _objects[i];
            if (obj.GetType().IsAssignableFrom(targetType))
            {
                if (targetName == null || obj.name == targetName)
                {
                    return obj;
                }
            }
        }
        Debug.Log("Not Found");

        return null;
    }

}
