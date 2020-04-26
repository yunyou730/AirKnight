using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class Entity
    {
        private static int s_idCounter = 0;
        int m_id = 0;

        public Entity()
        {
            m_id = ++s_idCounter;
        }

        public int GetID()
        {
            return m_id;
        }
    }
}
