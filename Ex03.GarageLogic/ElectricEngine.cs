using System;

namespace Ex03.GarageLogic
{
    public sealed class ElectricEngine : Engine
    {
        public override string ToString()
        {
            string electricDetails = string.Format(
@"Time remained to energy: {0}{1}",
            m_CurrentEnergy,
            Environment.NewLine);

            return electricDetails;
        }
    }
}