using System;
using System.Collections.Generic;

// Класс сущности
public class Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Класс для кэширования
public class Cache
{
    private static Cache instance;
    private Dictionary<Type, List<object>> cache;

    private Cache()
    {
        cache = new Dictionary<Type, List<object>>();
    }

    public static Cache GetInstance()
    {
        if (instance == null)
        {
            instance = new Cache();
        }
        return instance;
    }

    public void AddEntity<T>(T entity)
    {
        Type entityType = typeof(T);
        if (!cache.ContainsKey(entityType))
        {
            cache[entityType] = new List<object>();
        }
        cache[entityType].Add(entity);
    }

    public bool ContainsEntity<T>(T entity)
    {
        Type entityType = typeof(T);
        if (cache.ContainsKey(entityType))
        {
            return cache[entityType].Contains(entity);
        }
        return false;
    }

    public bool ContainsEntityType<T>()
    {
        Type entityType = typeof(T);
        return cache.ContainsKey(entityType);
    }

    public void RegisterEntityType<T>()
    {
        Type entityType = typeof(T);
        if (!cache.ContainsKey(entityType))
        {
            cache[entityType] = new List<object>();
        }
    }

    public void RemoveEntity<T>(T entity)
    {
        Type entityType = typeof(T);
        if (cache.ContainsKey(entityType))
        {
            cache[entityType].Remove(entity);
        }
    }

    public void RemoveEntityType<T>()
    {
        Type entityType = typeof(T);
        cache.Remove(entityType);
    }
}

class Program
{
    static void Main()
    {
        Cache cache = Cache.GetInstance();

        cache.RegisterEntityType<Entity>();

        Entity entity1 = new Entity { Id = 1, Name = "Entity 1" };
        Entity entity2 = new Entity { Id = 2, Name = "Entity 2" };

        cache.AddEntity(entity1);
        cache.AddEntity(entity2);

        Console.WriteLine("Contains Entity 1: " + cache.ContainsEntity(entity1));
        Console.WriteLine("Contains Entity 3: " + cache.ContainsEntity(new Entity { Id = 3, Name = "Entity 3" }));
        Console.WriteLine("Contains Entity type Entity: " + cache.ContainsEntityType<Entity>());

        cache.RemoveEntity(entity1);
        Console.WriteLine("Contains Entity 1 after removal: " + cache.ContainsEntity(entity1));

        cache.RemoveEntityType<Entity>();
        Console.WriteLine("Contains Entity type Entity after removal: " + cache.ContainsEntityType<Entity>());
    }
}
