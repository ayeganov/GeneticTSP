using System;

namespace GeneticTSP
{
    class RunningStat
    {
        private int m_num_values;
        private double m_old_mean, m_new_mean, m_old_std, m_new_std;

        public RunningStat()
        {
            Clear();
        }

        public void Clear()
        {
            m_num_values = 0;
            m_old_mean = m_new_mean = m_old_std = m_new_std = 0.0;
        }

        public void Push(double value)
        {
            ++m_num_values;

            if(m_num_values == 1)
            {
                m_old_mean = m_new_mean = value;
                m_old_std = 0.0;
            }
            else
            {
                m_new_mean = m_old_mean + (value - m_old_mean) / m_num_values;
                m_new_std = m_old_std + (value - m_old_mean) * (value - m_new_mean);

                m_old_mean = m_new_mean;
                m_old_std = m_new_std;
            }
        }

        public int NumValues
        {
            get
            {
                return m_num_values;
            }
        }

        public double Mean
        {
            get
            {
                return (m_num_values > 0) ? m_new_mean : 0.0;
            }
        }

        public double Variance
        {
            get
            {
                return (m_num_values > 1 ? m_new_std / (m_num_values - 1) : 0.0);
            }
        }

        public double StandardDeviation
        {
            get
            {
                return Math.Sqrt(Variance);
            }
        }
    }
}
