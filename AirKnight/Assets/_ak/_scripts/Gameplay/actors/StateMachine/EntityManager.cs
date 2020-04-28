using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class EntityManager
    {
        private static EntityManager s_instance = null;
        private Dictionary<int,BaseEntity>  m_entityMap = new Dictionary<int, BaseEntity>();

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

        public T CreateEntity<T>() where T: BaseEntity, new()
        {
            T t = new T();
            m_entityMap.Add(t.GetID(),t);
            return t;
        }
        
        public void UnRegisterEntity(int entityId)
        {
            m_entityMap.Remove(entityId);
        }

        public BaseEntity GetEntity(int entityId)
        {
            if(m_entityMap.ContainsKey(entityId))
            {
                return m_entityMap[entityId];
            }
            return null;
        }
    }

}
