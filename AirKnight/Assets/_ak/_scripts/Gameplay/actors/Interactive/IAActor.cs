using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class IAActor : MonoBehaviour
    {
        [SerializeField]
        BoxCollider2D  m_collider = null;

        [SerializeField]
        GameObject  m_hint = null;    

        [SerializeField]
        List<BeIA>  m_targetList = new List<BeIA>();

        [SerializeField]
        List<IAPlayer>  m_playerList = new List<IAPlayer>();

        void Awake()
        {
            m_hint.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            TryAddPlayer(collider);
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            TryAddPlayer(collider);
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            TryRemovePlayer(collider);
        }
        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position,"_ak/interactive_gear.png",true);
        }


        void Update()
        {
            bool bGetInput = CollectInput();
            if(bGetInput)
            {
                OnBeInteractivated();
            }
        }

        private bool CollectInput()
        {
            foreach(IAPlayer player in m_playerList)
            {
                if(player.CheckIAButon())
                {
                    return true;
                }
            }
            return false;
        }

        private void OnBeInteractivated()
        {
            Debug.Log("===OnBeInteractivated===");
            foreach(BeIA beIA in m_targetList)
            {
                beIA.OnBeInteracted();
            }
        }

        private void TryAddPlayer(Collider2D collider)
        {
            IAPlayer player = collider.GetComponent<IAPlayer>();
            if(player != null && m_playerList.IndexOf(player) == -1)
            {
                if(m_playerList.Count == 0)
                {
                    ToggleToOn();
                }
                m_playerList.Add(player);
            }
        }   

        private void TryRemovePlayer(Collider2D collider)
        {
            IAPlayer player = collider.GetComponent<IAPlayer>();
            if(player != null)
            {
                int holdIndex = m_playerList.IndexOf(player);
                if(holdIndex >= 0)
                {
                    m_playerList.RemoveAt(holdIndex);
                    if(m_playerList.Count == 0)
                    {
                        ToggleToOff();
                    }                    
                }
            }
        }

        private void RemoveAllPlayers()
        {
            m_playerList.RemoveAll((IAPlayer player)=>{
                return true;
            });
        }

        private void ToggleToOn()
        {
            m_hint.SetActive(true);
        }

        private void ToggleToOff()
        {
            m_hint.SetActive(false);
        }
    }
}
