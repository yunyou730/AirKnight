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
        TimeMachine m_timeMachine = new TimeMachine();
        
        public void Update()
        {
            RefreshFPS(Time.deltaTime);
            m_timeMachine.Update();
        }


        private void RefreshFPS(float dt)
        {
            float fps = 1.0f / dt;
            m_strFPS.Clear();
            m_strFPS.Append("FPS:");
            m_strFPS.Append(fps.ToString());
            m_fpsLabel.text = m_strFPS.ToString();
        }

        public void SetFPS()
        {
            Application.targetFrameRate = 30;
        }


        public void OnClickRecord()
        {
            m_timeMachine.StartRecord();
        }

        public void OClickPlayBack()
        {
            m_timeMachine.StartPlayBack();
        }

        public void OnClickRegisterAries()
        {
            Debug.Log("RegisterAries");
            Recorder recorder = GameObject.Find("Aries").GetComponent<AriesRecorder>();
            m_timeMachine.RegisterRecorder(recorder);
        }

    }
}
