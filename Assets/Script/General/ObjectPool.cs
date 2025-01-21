using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();  // 存储可复用的对象队列
    private T prefab;  // 对象的预设（Prefab）

    // 构造函数，传入对象的预设
    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
    }

    // 从池中获取一个对象
    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();  // 获取对象
            obj.gameObject.SetActive(true);  // 激活对象
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(prefab);  // 如果池为空，创建新对象
            return newObj;
        }
    }

    // 将对象归还到池中
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);  // 禁用对象
        pool.Enqueue(obj);  // 将对象放回池中
    }

    // 清空池
    public void ClearPool()
    {
        pool.Clear();
    }
}