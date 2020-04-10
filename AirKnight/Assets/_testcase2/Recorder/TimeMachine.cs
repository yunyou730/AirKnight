using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class TimeMachine
    {
        
        public enum State
        {
            Record,
            PlayBack,
        }

        State m_state = State.Record;

        int m_counter = 0;
        Dictionary<int, RecorderItem> m_recorderMap = new Dictionary<int, RecorderItem>();

        public void RegisterRecorder(Recorder recorder)
        {
            m_counter++;
            RecorderItem item = new RecorderItem(m_counter,recorder);
            m_recorderMap.Add(item.GetId(),item);
        }

        public void StartPlayBack()
        {
            m_state = State.PlayBack;
        }

        public void StartRecord()
        {
            m_state = State.Record;
        }

        public void Update()
        {
            switch (m_state)
            {
                case State.Record:
                    {
                        foreach (RecorderItem item in m_recorderMap.Values)
                        {
                            item.Record();
                        }
                    }
                    break;
                case State.PlayBack:
                    {
                        foreach (RecorderItem item in m_recorderMap.Values)
                        {
                            item.PlayBack();
                        }
                    }
                    break;
            }
        }
    }

    class RecorderItem
    {
        public int m_id = 0;
        public Recorder m_recorder = null;
        public List<TimeFrame> m_frames = new List<TimeFrame>();

        public RecorderItem(int id,Recorder recorder)
        {
            m_id = id;
            m_recorder = recorder;
        }

        public int GetId()
        {
            return m_id;
        }

        public void PlayBack()
        {
            if (m_frames.Count > 0)
            {
                int index = m_frames.Count - 1;
                TimeFrame frame = m_frames[index];
                m_recorder.PlayBack(frame);
                m_frames.RemoveAt(index);
            }
        }

        
        public void Record()
        {
            TimeFrame frame = m_recorder.DoRecord();
            m_frames.Add(frame);
        }
    }
}

