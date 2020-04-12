using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesBeHit : CanBeHit
    {
        SpriteRenderer m_spriteRenderer = null;

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public override void OnBeHit(GameObject caster)
        {
            Debug.Log("AriesBeHit() [caster] " + caster.name + " [self] " + gameObject.name);
            m_spriteRenderer.color = new Color(1, 0, 0, 1);
            StartCoroutine(RecoverColor());
        }


        IEnumerator RecoverColor()
        {
            yield return new WaitForSeconds(0.5f);
            m_spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
}
