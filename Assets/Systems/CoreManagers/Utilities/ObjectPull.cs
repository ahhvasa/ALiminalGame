using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPull<T> where T : Object
{
    protected Queue<T> pool; // Очередь для хранения объектов
    protected T prefab;      // Префаб, который будет инстанцироваться
    protected Transform parent; // Родительский объект для всех объектов пула (опционально)

    // Конструктор для инициализации пула
    public ObjectPull(T prefab, int initialSize = 10, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<T>(initialSize);

        // Заполняем пул объектами
        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateObject();
            DeactivateObject(obj); // Делаем объект неактивным
            pool.Enqueue(obj);     // Добавляем в пул
        }
    }

    // Создает объект через Instantiate
    protected virtual T CreateObject()
    {
        T obj = Object.Instantiate(prefab, parent);
        return obj;
    }

    // Метод для получения объекта из пула
    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            ActivateObject(obj); // Активируем объект перед возвратом
            return obj;
        }
        else
        {
            // Если пул пуст, создаем новый объект
            T obj = CreateObject();
            ActivateObject(obj);
            return obj;
        }
    }

    // Метод для возврата объекта в пул
    public void ReturnObject(T item)
    {
        DeactivateObject(item); // Деактивируем объект
        pool.Enqueue(item);     // Возвращаем в очередь
    }

    // Активирует объект (для GameObject или других компонентов)
    protected void ActivateObject(T item)
    {
        if (item is GameObject go)
        {
            go.SetActive(true);
        }
        else if (item is Component component)
        {
            component.gameObject.SetActive(true);
        }
    }

    // Деактивирует объект (для GameObject или других компонентов)
    protected void DeactivateObject(T item)
    {
        if (item is GameObject go)
        {
            go.SetActive(false);
        }
        else if (item is Component component)
        {
            component.gameObject.SetActive(false);
        }
    }
}


public class ObjectPull_SelfReturningItem<T> : ObjectPull<T>
where T : Object, IObjectPull_SelfReturningItem_Item
{
	public ObjectPull_SelfReturningItem(T prefab, int initialSize = 10, Transform parent = null) : base(prefab, initialSize, parent)
	{}
	
	protected override T CreateObject()
    {
        T obj = Object.Instantiate(prefab, parent);
		(obj as IObjectPull_SelfReturningItem_Item).ReturnAction = () => ReturnObject(obj);
		UnityEngine.Debug.Log((obj as IObjectPull_SelfReturningItem_Item).ReturnAction == null);
        return obj;
    }
}

public interface IObjectPull_SelfReturningItem_Item
{
	System.Action ReturnAction { get; set; }
}