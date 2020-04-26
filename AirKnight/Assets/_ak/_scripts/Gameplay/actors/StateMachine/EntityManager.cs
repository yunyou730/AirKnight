using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class EntityManager
    {
        private static EntityManager s_instance = null;
        private Dictionary<int,Entity>  m_entityMap = new Dictionary<int, Entity>();

        public static EntityManager GetInstance()
        {
            if(s_instance == null)
            {
                s_instance = new EntityManager();
            }
            return EntityManager.s_instance;
        }

        public static void CleanUp()
        {
            if(s_instance != null)
            {   
                s_instance = null;
            }
        }

        private EntityManager()
        {
        }

        public T CreateEntity<T>() where T:Entity,new()
        {
            T t = new T();
            m_entityMap.Add(t.GetID(),t);
            return t;
        }
        
        public void UnRegisterEntity(int entityId)
        {
            m_entityMap.Remove(entityId);
        }

        public Entity GetEntity(int entityId)
        {
            if(m_entityMap.ContainsKey(entityId))
            {
                return m_entityMap[entityId];
            }
            return null;
        }
    }

}
