using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

namespace ff
{

    public class GameEntry : MonoBehaviour
    {
        public Text m_fpsLabel = null;
        StringBuilder m_strFPS = new StringBuilder();
        
        public void Update()
        {
            float fps = 1.0f / Time.deltaTime;
            m_strFPS.Clear();
            m_strFPS.Append("FPS:");
            m_strFPS.Append(fps.ToString());
            m_fpsLabel.text = m_strFPS.ToString();
        }
    }
}
