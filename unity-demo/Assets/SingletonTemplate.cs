//Author@ qiuyukun

/// <summary>
/// 单例模版
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonTemplate<T> where T : class, new() 
{
    /// <summary>
    /// 实例
    /// </summary>
    public static T Inst
    {
        get
        {
            return _inst ?? (_inst = new T());
        }
    }
    private static T _inst;
}
