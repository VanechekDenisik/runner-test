using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Helpers
{
    public static class SafeEditorHelper
    {
        /// <summary>
        ///     Make asset dirty
        /// </summary>
        /// <typeparam name="T">Asset of base unity.object type</typeparam>
        public static void SetAssetDirty<T>(T asset) where T : Object
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(asset);
#endif
        }

        /// <summary>
        ///     Create Asset On Specific path
        /// </summary>
        /// <param name="path">Full path to asset without "Asset" root folder and extension</param>
        /// <typeparam name="T">Type of asset</typeparam>
        /// <returns></returns>
        public static T CreateAssetOfType<T>(string path) where T : ScriptableObject
        {
            var asset = CreateAssetOfType(typeof(T), path);

            if (asset != null)
                return (T)asset;

            return null;
        }

        public static Object CreateAssetOfType(Type type, string path)
        {
#if UNITY_EDITOR
            var filePath = $"{path}.asset";

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

            Object asset = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(asset, filePath);
            AssetDatabase.SaveAssets();

            return asset;
#endif
            return null;
        }

        /// <summary>
        ///     Create Asset at path next to another asset at the same folder
        /// </summary>
        /// <param name="asset">Asset for getting path</param>
        /// <param name="newAssetName">New asset name</param>
        /// <typeparam name="T">Type of asset</typeparam>
        /// <returns></returns>
        public static T CreateAssetOfTypeNextToAnotherAsset<T>(Object asset, string newAssetName)
            where T : ScriptableObject
        {
            var path = GetAssetPath(asset);
            return CreateAssetOfType<T>(path + newAssetName);
        }

        /// <summary>
        ///     Get asset path as string
        /// </summary>
        /// <param name="asset">Asset for getting path</param>
        /// <returns>Asset path as string</returns>
        public static string GetAssetPath(Object asset)
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(asset);
            var assetName = Path.GetFileName(path);
            return path.Replace(assetName, "");
#endif
            return null;
        }

        /// <summary>
        ///     Get asset name with extention
        /// </summary>
        /// <param name="asset">Asset for getting name</param>
        /// <returns></returns>
        public static string GetAssetName(Object asset)
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(asset);
            return Path.GetFileName(path);
#endif
            return null;
        }

        /// <summary>
        ///     Get asset name without extention
        /// </summary>
        /// <param name="asset">Asset for getting name</param>
        /// <returns></returns>
        public static string GetAssetNameWithoutExtension(Object asset)
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(asset);
            return Path.GetFileNameWithoutExtension(path);
#endif
            return null;
        }

        /// <summary>
        ///     Remove all assets in folder
        /// </summary>
        /// <param name="path">Path to folder</param>
        public static void RemoveAssetsAtPath(string path)
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset($"{path}");
            AssetDatabase.SaveAssets();
#endif
        }

        /// <summary>
        ///     Remove asset from project
        /// </summary>
        public static void RemoveAsset(Object asset)
        {
#if UNITY_EDITOR
            if (asset == null) return;
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
            AssetDatabase.SaveAssets();
#endif
        }

        public static T[] GetAssetsOfType<T>() where T : Object
        {
            List<T> result = new();

#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                result.Add(asset);
            }
#endif
            return result.ToArray();
        }

        public static T GetAssetOfType<T>() where T : Object
        {
            var assets = GetAssetsOfType<T>();
            return assets.Length > 0 ? assets[0] : default;
        }

        public static T GetAssetOfType<T>(string assetName) where T : Object
        {
            var assets = GetAssetsOfType<T>();

            foreach (var asset in assets)
                if (asset.name == assetName)
                    return asset;

            return null;
        }
    }
}