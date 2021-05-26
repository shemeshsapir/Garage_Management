using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EngineCapacity;
        protected float m_CurrentEnergy;
        protected float m_EnergyPercentage;

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
                m_EnergyPercentage = (m_CurrentEnergy / m_EngineCapacity) * 100;
            }
        }

        public float EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergyPercentage;
            }
        }

        public void UpdateEnergy(float i_AmountToAdd)
        {
            if (i_AmountToAdd + m_CurrentEnergy <= m_EngineCapacity)
            {
                m_CurrentEnergy += i_AmountToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_EngineCapacity - m_CurrentEnergy);
            }
        }

        public void UpdateEnergyPercentage()
        {
            m_EnergyPercentage = (m_CurrentEnergy / m_EngineCapacity) * 100;
        }
    }
}