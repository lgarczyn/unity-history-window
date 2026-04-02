using System;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace Gemserk
{
    public static class LazyLoadReferenceExtensions
    {
        public static string GetAssetName<T>(this LazyLoadReference<T> reference)
            where T : Object
        {
            // Get name without loading asset
    #if UNITY_EDITOR
            int instanceId = reference.instanceID;
            string path = AssetDatabase.GetAssetPath(instanceId);
            if (String.IsNullOrEmpty(path))
            {
                return null;
            }

            string name = Path.GetFileNameWithoutExtension(path);
            return name;
    #else
            Object asset = reference.asset;
            return asset ? asset.name : null;
    #endif
        }

        public static Type GetAssetType<T>(this LazyLoadReference<T> reference)
            where T : Object
        {
            // Get type without loading asset
    #if UNITY_EDITOR
            int instanceId = reference.instanceID;
            string path = AssetDatabase.GetAssetPath(instanceId);
            if (String.IsNullOrEmpty(path))
            {
                return null;
            }

            return AssetDatabase.GetMainAssetTypeAtPath(path);
    #else
            Object asset = reference.asset;
            return asset ? asset.GetType() : null;
    #endif
        }
    }
}
