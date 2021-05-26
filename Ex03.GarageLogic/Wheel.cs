namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public enum eMaxAirPressure
        {
            Car = 32,
            Motorcycle = 30,
            Truck = 26
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public void InflateWheel(float i_AmountToAdd)
        {
            if (i_AmountToAdd + m_CurrentAirPressure <= r_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AmountToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            string wheelDetails = string.Format(
@" | Manufacturer name: {0} | Current air pressure: {1}",
            m_ManufacturerName,
            m_CurrentAirPressure);

            return wheelDetails;
        }
    }
}