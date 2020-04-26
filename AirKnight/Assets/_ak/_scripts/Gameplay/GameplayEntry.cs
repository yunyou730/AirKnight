using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

namespace ff
{

    public class GameplayEntry : MonoBehaviour
    {
        public Text m_fpsLabel = null;
        StringBuilder m_strFPS = new StringBuilder();
        TimeMachine m_timeMachine = new TimeMachine();


        private void Awake()
        {
            Physics2D.queriesStartInColliders = false;
            EntityManager.GetInstance();
        }

        public void Update()
        {
            RefreshFPS(Time.deltaTime);
            m_timeMachine.Update();
        }

        void Destroy()
        {
            EntityManager.CleanUp();
        }

        private void RefreshFPS(float dt)
        {
            float fps = 1.0f / dt;
            m_strFPS.Clear();
            m_strFPS.Append("FPS:");
            m_strFPS.Append(fps.ToString());
            if (m_fpsLabel)
            {
                m_fpsLabel.text = m_strFPS.ToString();
            }
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
