namespace Ex03.GarageLogic
{
    public sealed class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler = 1, 
            Octan95, 
            Octan96, 
            Octan98
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public override string ToString()
        {
            string fuelDetails = string.Format(
@"Type of fuel: {0}
Amount of fuel left: {1}",
            m_FuelType.ToString(),
            m_CurrentEnergy);

            return fuelDetails;
        }
    }
}