using UnityEngine;

// シングルトンを使用するクラスでこれを継承する
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    protected virtual void Awake()
    {
        if (instance == null)
            instance = (T)FindObjectOfType(typeof(T));
        else
            Destroy(gameObject);
    }
}