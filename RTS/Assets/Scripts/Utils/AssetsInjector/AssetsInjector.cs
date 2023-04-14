using System;
using System.Collections.Generic;
using System.Reflection;

public static class AssetsInjector
{
    private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

    public static T Inject<T>(this AssetsContext context, T target)
    {
        var targetType = target.GetType();
        List<FieldInfo> allFields = new List<FieldInfo>();

        while (targetType != null)
        {
            allFields.AddRange(GetFieldsFromType(targetType));
            targetType = targetType.BaseType;
        }

        for (int i = 0; i < allFields.Count; i++)
        {
            var fieldInfo = allFields[i];
            var injectAssetAttribute =
                fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
            if (injectAssetAttribute == null)
            {
                continue;
            }
            var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
            fieldInfo.SetValue(target, objectToInject);
        }

        return target;
    }

    private static FieldInfo[] GetFieldsFromType(Type targetType)
    {
        return targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public
            | BindingFlags.Instance);
    }
}

