using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private const int k_MinChoiceEnums = 1;
        private const int k_MaxVehicleChoice = 3;
        private const int k_MaxFuelChoice = 4;
        private const float k_MinEnergyAmount = 0;
        private readonly GarageManager r_GarageManager = new GarageManager();
        private int m_MaxMenuOption = Enum.GetValues(typeof(eGarageOptions)).Length;

        public enum eGarageOptions
        {
            InsertVehicleToGarage = 1,
            ShowListOfVehiclesInGarage,
            ChangeVehicleStatus,
            InflateWheelsToMaximum,
            IncreaseEnergyInVehicle,
            PrintVehicleDetails,
            Exit
        }

        public void OpenGarage()
        {
            int userChoice = -1;

            while (userChoice != (int)eGarageOptions.Exit)
            {
                try
                {
                    printMainMenu();
                    userChoice = getOptionChoiceFromUser();
                    performUserChoice(userChoice);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong input format!");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void printMainMenu()
        {
            string welcomeMessage = string.Format(
@"Welcome to our Garage management! We are GLAD to serve you!
Choose any of the options below:
=============================================================
1. Insert new vehicle to the garage
2. Show list of vehicles in the garage
3. Change vehicle's status
4. Inflate wheel's air to maximum
5. Increase energy in vehicle engine
6. Print vehicle details
7. Exit");

            Console.WriteLine(welcomeMessage);
            Console.WriteLine();
        }

        private int getOptionChoiceFromUser()
        {
            int userChoice = -1;
            string inputFromUser = string.Empty;

            try
            {
                inputFromUser = Console.ReadLine();
                userChoice = int.Parse(inputFromUser);
                checkUserInputValidation(userChoice, k_MinChoiceEnums, m_MaxMenuOption);
            }
            catch (FormatException)
            {
                throw new FormatException("Illegal input's format type!");
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }
            finally
            {
                Console.Clear();
            }

            return userChoice;
        }

        private void performUserChoice(int i_UserChoice)
        {
            eGarageOptions userChoice = (eGarageOptions)i_UserChoice;

            try
            {
                switch (userChoice)
                {
                    case eGarageOptions.InsertVehicleToGarage:
                        insertNewVehicle();
                        break;
                    case eGarageOptions.ShowListOfVehiclesInGarage:
                        showListOfVehiclesInGarage();
                        break;
                    case eGarageOptions.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eGarageOptions.InflateWheelsToMaximum:
                        inflateWheelsToMaximum();
                        break;
                    case eGarageOptions.IncreaseEnergyInVehicle:
                        increaseEngineEnergy();
                        break;
                    case eGarageOptions.PrintVehicleDetails:
                        printVehicleDetails();
                        break;
                    case eGarageOptions.Exit:
                        Console.WriteLine("It's always a pleasure to serve you!{0}Come back later... Goodby! :)", Environment.NewLine);
                        break;
                }
            }
            catch(FormatException ex)
            {
                throw ex;
            }
        }

        private void insertNewVehicle()
        {
            StringBuilder uniqueMessage = new StringBuilder();
            string licenseNumber = getLicenseNumber();
            checkIfStringEmpty(licenseNumber);
            Vehicle newVehicle = r_GarageManager.FindVehicleByLicenseNumber(licenseNumber);

            if (newVehicle == null)
            {
                try
                {
                    newVehicle = CreateNewVehicle(licenseNumber);
                    getVehicleDetails(newVehicle);
                    uniqueMessage.AppendFormat("Vehicle {0} added to the garage!", licenseNumber);
                }
                catch (Exception ex)
                {
                    r_GarageManager.VehicleInGarage.Remove(newVehicle);
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                uniqueMessage.AppendFormat("This vehicle is already exist in the garage!");
                r_GarageManager.ChangeVehicleStatus(newVehicle, Vehicle.eVehicleGarageStatus.InRepair);
            }

            Console.WriteLine(uniqueMessage);
            waitForUserInput();
        }

        private Vehicle CreateNewVehicle(string i_LicenseNumber)
        {
            int vehicleType = 0, isElectric = -1;
            Vehicle newVehicle = null;
            string vehicleTypesMessage = string.Format(
@"{0}Please choose one of the options below:
1. Car
2. Motorcycle
3. Truck{0}",
            Environment.NewLine);

            Console.WriteLine(vehicleTypesMessage);

            try
            {
                vehicleType = int.Parse(Console.ReadLine());
                checkUserInputValidation(vehicleType, k_MinChoiceEnums, k_MaxVehicleChoice);
                isElectric = getVehicleTypeFromUser();

                newVehicle = VehicleFactory.CreatVehicle(i_LicenseNumber, vehicleType, isElectric - 1);
                r_GarageManager.AddVehicleToGarage(newVehicle);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newVehicle;
        }

        private void getVehicleDetails(Vehicle i_VehicleToUpdate)
        {
            int wheelsChoice = -1;
            float currentAmountEnergy = 0;
            string currentWheelStr = string.Empty, currentPressureStr = string.Empty, property = string.Empty;
            List<string> specificProperties = i_VehicleToUpdate.GetSpecificVehicleDetails();
            List<string> specificAnswers = new List<string>();

            i_VehicleToUpdate.ModelName = getModelName();
            i_VehicleToUpdate.OwnerName = getOwnerName();
            i_VehicleToUpdate.OwnerPhone = getOwnerPhoneNumber();
            int.Parse(i_VehicleToUpdate.OwnerPhone);
            wheelsChoice = wheelsAddingOptions();
            
            switch (wheelsChoice)
            {
                case 1:
                    getWheelsOneByOne(i_VehicleToUpdate);
                    break;
                case 2:
                    getWheelsAllTogether(i_VehicleToUpdate);
                    break;
            }

            Console.Write("Current amount of energy: ");

            try
            {
                currentAmountEnergy = float.Parse(Console.ReadLine());
                checkUserInputValidation(currentAmountEnergy, k_MinEnergyAmount, i_VehicleToUpdate.VehicleEngine.EngineCapacity);
                i_VehicleToUpdate.VehicleEngine.CurrentEnergy = currentAmountEnergy;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }

            foreach (string vehicleProperty in specificProperties)
            {
                Console.WriteLine(vehicleProperty);
                property = Console.ReadLine();
                specificAnswers.Add(property);
            }

            i_VehicleToUpdate.SetSpecificDetails(specificAnswers);
        }

        private void getWheelsAllTogether(Vehicle i_VehicleToUpdate)
        {
            Console.Write("Enter Manufacturer name: ");
            string manufacturerName = Console.ReadLine();
            checkIfStringEmpty(manufacturerName);
            Wheel wheelMaxAir = i_VehicleToUpdate.Wheels[0];

            Console.Write("Enter air pressure: ");
            float currentAirPressure = checkAirPressureValidation(wheelMaxAir.MaxAirPressure);

            foreach (Wheel wheel in i_VehicleToUpdate.Wheels)
            {
                wheel.ManufacturerName = manufacturerName;
                wheel.CurrentAirPressure = currentAirPressure;
            }
        }

        private void getWheelsOneByOne(Vehicle i_VehicleToUpdate)
        {
            int i = 1;
            string currentManufacturer = string.Empty, currentAirPressureStr = string.Empty;
            float currentAirPressure = 0;

            foreach (Wheel wheel in i_VehicleToUpdate.Wheels)
            {
                currentManufacturer = getManufacturerName(i);
                wheel.ManufacturerName = currentManufacturer;

                Console.Write("Wheel {0} Current air pressure: ", i);
                currentAirPressure = checkAirPressureValidation(wheel.MaxAirPressure);

                wheel.CurrentAirPressure = currentAirPressure;
                i++;
            }
        }

        private float checkAirPressureValidation(float i_MaxAirPressure)
        {
            string currentAirPressureStr = Console.ReadLine();
            float currentAirPressure = 0;

            if (!float.TryParse(currentAirPressureStr, out currentAirPressure))
            {
                throw new FormatException();
            }

            if (currentAirPressure > i_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure);
            }

            return currentAirPressure;
        }

        private string getManufacturerName(int i_WheelIndex)
        {
            Console.Write("Wheel {0} Manufacturer name: ", i_WheelIndex);
            string inputFromUser = Console.ReadLine();
            checkIfStringEmpty(inputFromUser);

            return inputFromUser;
        }

        private int wheelsAddingOptions()
        {
            bool isValidInput = false;
            string message = string.Format(
@"Choose action for adding wheels:
1. One by One
2. All together{0}",
            Environment.NewLine);
            Console.WriteLine(message);

            isValidInput = int.TryParse(Console.ReadLine(), out int userChoice);

            while (!isValidInput || (userChoice != 1 && userChoice != 2))
            {
                Console.WriteLine("Please enter only 1 or 2:");
                isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }

        private int getVehicleTypeFromUser()
        {
            int userChoice = -1;
            string message = string.Format(
@"{0}Choose vehicle type:
1. Fuel
2. Electric{0}", 
            Environment.NewLine);

            Console.WriteLine(message);
            
            userChoice = int.Parse(Console.ReadLine());
            if (userChoice < 1 || userChoice > 2)
            {
                throw new ValueOutOfRangeException(1, 2);
            }

            return userChoice;
        }

        private string getModelName()
        {
            Console.WriteLine();
            Console.Write("Vehicle's Model name: ");
            string inputFromUser = Console.ReadLine();
            checkIfStringEmpty(inputFromUser);

            return inputFromUser;
        }

        private string getOwnerName()
        {
            Console.Write("Vehicle's Owner name: ");
            string inputFromUser = Console.ReadLine();
            checkIfStringEmpty(inputFromUser);

            return inputFromUser;
        }

        private string getOwnerPhoneNumber()
        {
            Console.Write("Owner's Phone number: ");
            string inputFromUser = Console.ReadLine();
            Console.WriteLine();
            checkIfStringEmpty(inputFromUser);

            return inputFromUser;
        }

        private void showListOfVehiclesInGarage()
        {
            bool isFilter = isFilteringVehicleResults();
            int userChoice = -1;

            if (isFilter)
            {
                userChoice = getStatusToFilterBy();
                printFilteredListOfVehicles(userChoice);
            }
            else
            {
                printAllVehiclesList();
            }

            waitForUserInput();
        }

        private void changeVehicleStatus()
        {
            Vehicle vehicleToUpdateStatus = getVehicle();
            int statusToSet = -1;

            if (vehicleToUpdateStatus != null)
            {
                statusToSet = getStatusToSetFromUser();
                r_GarageManager.ChangeVehicleStatus(vehicleToUpdateStatus, (Vehicle.eVehicleGarageStatus)statusToSet);
                Console.WriteLine("Status changed succesfully!");
            }

            waitForUserInput();
        }

        private void inflateWheelsToMaximum()
        {
            Vehicle currentVehicle = getVehicle();

            if (currentVehicle != null)
            {
                currentVehicle.InflateWheel();
                Console.WriteLine("Wheel's air filled to the maximum!");
            }

            waitForUserInput();
        }

        private void increaseEngineEnergy()
        {
            Vehicle currentVehicle = getVehicle();

            if (currentVehicle != null)
            {
                if (currentVehicle.VehicleEngine is FuelEngine)
                {
                    refuellingFuelVehicle(currentVehicle);
                }
                else
                {
                    chargingElectricVehicle(currentVehicle);
                }

                currentVehicle.VehicleEngine.UpdateEnergyPercentage();
            }
        }

        private void refuellingFuelVehicle(Vehicle i_VehicleToRefuel)
        {
            float amount = 0;
            float fuelChoice = 0;
            StringBuilder message = new StringBuilder();

            try
            {
                fuelChoice = getFuelType();

                if ((i_VehicleToRefuel.VehicleEngine as FuelEngine).FuelType != (FuelEngine.eFuelType)fuelChoice)
                {
                    throw new ArgumentException("The fuel type you chose doesn't match the vehicle's fuel type!");
                }

                amount = getAmountToFill();
                i_VehicleToRefuel.VehicleEngine.UpdateEnergy(amount);
                message.AppendFormat("Fuel tunk refilled succesfully!{0}", Environment.NewLine);
                Console.WriteLine(message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            waitForUserInput();
        }

        private void chargingElectricVehicle(Vehicle i_VehicleToCharge)
        {
            float amount = getAmountToFill();
            StringBuilder message = new StringBuilder();

            try
            {
                i_VehicleToCharge.VehicleEngine.UpdateEnergy(amount);
                message.AppendFormat("Battery charged succesfully!{0}Current energy percentage: {1}%", Environment.NewLine, i_VehicleToCharge.EnergyPercentage);
                Console.WriteLine(message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            waitForUserInput();
        }

        private void printVehicleDetails()
        {
            Vehicle currentVehicle = getVehicle();

            if (currentVehicle != null)
            {
                Console.WriteLine(currentVehicle.ToString());
            }

            waitForUserInput();
        }

        private Vehicle getVehicle()
        {
            string inputLicenseNumber = getLicenseNumber();
            Vehicle currentVehicle = r_GarageManager.FindVehicleByLicenseNumber(inputLicenseNumber);

            if (currentVehicle == null)
            {
                Console.WriteLine("There is no such vehicle in the garage!");
            }

            return currentVehicle;
        }

        private float getAmountToFill()
        {
            Console.Write("Please enter the amount of energy you want to add: ");
            return float.Parse(Console.ReadLine());
        }

        private string getLicenseNumber()
        {
            Console.WriteLine("Please enter license number:");
            return Console.ReadLine();
        }

        private int getStatusToSetFromUser()
        {
            int statusToSet = -1;
            string message = string.Format(
@"Please choose one of the following status options:
1. InRepair
2. Repaired
3. Paid");

            Console.WriteLine(message);
            statusToSet = int.Parse(Console.ReadLine());

            if (!Enum.IsDefined(typeof(Vehicle.eVehicleGarageStatus), statusToSet))
            {
                throw new ValueOutOfRangeException(k_MinChoiceEnums, Enum.GetValues(typeof(Vehicle.eVehicleGarageStatus)).Length);
            }

            return statusToSet;
        }

        public bool isFilteringVehicleResults()
        {
            bool isValidInput = false, isFilter = false;
            string inputFromUser = string.Empty;
            int userChoice = -1;
            string message = string.Format(
@"Do you want to filter vehicle's results by status?
1. YES
2. NO");
            Console.WriteLine(message);
            inputFromUser = Console.ReadLine();
            isValidInput = int.TryParse(inputFromUser, out userChoice);

            while (!isValidInput || (userChoice != 1 && userChoice != 2))
            {
                Console.WriteLine("Wrong input! please choose only 1 or 2:");
                inputFromUser = Console.ReadLine();
                isValidInput = int.TryParse(inputFromUser, out userChoice);
            }

            isFilter = userChoice == 1 ? true : false;
            
            return isFilter;
        }

        private float getFuelType()
        {
            float fuelChoice = 0;
            string fuelTypes = string.Format(
@"Choose Fuel type:
1. Soler
2. Octan95
3. Octan96
4. Octan98");

            Console.WriteLine(fuelTypes);
            fuelChoice = int.Parse(Console.ReadLine());
            checkUserInputValidation(fuelChoice, k_MinChoiceEnums, k_MaxFuelChoice);

            return fuelChoice;
        }

        private int getStatusToFilterBy()
        {
            bool isValidInput = false;
            int userChoice = -1;
            string inputFromUser = string.Empty;
            string statusTypes = string.Format(
@"Choose one of the options below for filtering vehiclee:
1. InRepair
2. Repaired
3. Paid");

            Console.WriteLine(statusTypes);
            inputFromUser = Console.ReadLine();
            isValidInput = int.TryParse(inputFromUser, out userChoice);

            while (!isValidInput || (userChoice < k_MinChoiceEnums || userChoice > Enum.GetValues(typeof(Vehicle.eVehicleGarageStatus)).Length))
            {
                Console.WriteLine("Wrong input! Please enter a valid choice");
                isValidInput = int.TryParse(inputFromUser, out userChoice);
            }

            return userChoice;
        }

        private void printFilteredListOfVehicles(int i_StatusToPrint)
        {
            List<string> filteredVehicles = r_GarageManager.FilterVehiclesByStatus(i_StatusToPrint);
            StringBuilder listOfVehicles = new StringBuilder();

            if (filteredVehicles.Count == 0)
            {
                listOfVehicles.Append("There is no such vehicles on this status!");
            }
            else
            {
                listOfVehicles.AppendFormat("Filtered by status: {0}", (Vehicle.eVehicleGarageStatus)i_StatusToPrint).AppendLine();

                foreach (string licenseNumber in filteredVehicles)
                {
                    listOfVehicles.Append("License number: ").AppendLine(licenseNumber);
                }
            }

            Console.WriteLine(listOfVehicles);
        }

        private void printAllVehiclesList()
        {
            List<Vehicle> vehicleList = r_GarageManager.VehicleInGarage;
            StringBuilder listOfVehiclesToPrint = new StringBuilder();

            if (vehicleList.Count == 0)
            {
                listOfVehiclesToPrint.AppendLine("There is no vehicles in the garage!");
            }
            else
            {
                foreach (Vehicle vehicle in vehicleList)
                {
                    listOfVehiclesToPrint.AppendFormat("Vehicle's License number: {0}, status: {1}", vehicle.LicenseNumber, vehicle.VehicleStatus);
                    listOfVehiclesToPrint.AppendLine();
                }
            }

            Console.WriteLine(listOfVehiclesToPrint);
        }

        private void checkUserInputValidation(float i_InputToCheck, float i_MinValue, float i_MaxValue)
        {
            if (i_InputToCheck > i_MaxValue || i_InputToCheck < i_MinValue)
            {
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
            }
        }

        private void checkIfStringEmpty(string i_Input)
        {
            if (i_Input == string.Empty)
            {
                throw new ArgumentNullException();
            }
        }

        private void waitForUserInput()
        {
            Console.WriteLine("Press any key to return the main menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}