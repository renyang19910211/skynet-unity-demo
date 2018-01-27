//Author@ qiuyukun

using UnityEngine;
using System.Collections;

/// <summary>
/// 基于MonoBehaviour的单例模版
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBehaviour<T> : MonoBehaviour where T : Object
{
    /// <summary>
    /// 实例
    /// </summary>
    public static T inst { get {
            return _inst ?? (_inst = GameObject.FindObjectOfType<T>());
        } }

    private static T _inst;
}
