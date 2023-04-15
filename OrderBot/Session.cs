using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOME, OPTIONSELECTION, APPOINTMENT_BOOKING, TESTDRIVE, CHANGEAPPOINTMENT, ORDERINFO
        }

        private State currentState = State.WELCOME;

        private Order order;

        public Session(string sPhone)
        {
            order = new Order();
        }

        public List<string> OnMessage(string inputMessage)
        {
            var listOfSupportedWelcomeOptions = new List<string> { "1", "2", "3", "4" };
            var messages = new List<string>();

            switch (currentState)
            {
                case State.WELCOME:
                    messages.Add("Welcome to CarGenie Bot!");
                    messages.Add("I can help you today with the below options. Please select one.");
                    messages.Add("Book an appointment - To select press 1");
                    messages.Add("Book a test drive  - To select press 2");
                    messages.Add("Change an appointment  - To select press 3");
                    messages.Add("Information about your order  - To select press 4");
                    currentState = State.OPTIONSELECTION;
                    break;
                case State.OPTIONSELECTION:
                    if(!listOfSupportedWelcomeOptions.Contains(inputMessage))
                    {
                        messages.Add("Unsupported Option. Press any key to go back to the welcome page");
                        currentState = State.WELCOME;
                    }
                    else
                    {
                        messages.AddRange(GetOptionSelection(inputMessage));
                        currentState = GetStateFromOptionSelection(inputMessage);
                    }
                    
                    break;
                case State.APPOINTMENT_BOOKING:
                    messages.Add(BookAppointment(inputMessage));
                    currentState = State.WELCOME;
                    break;
                case State.TESTDRIVE:
                    var mess = BookTestDrive(inputMessage);
                    messages.Add(mess);
                    if(!mess.StartsWith("Test"))
                    {
                        currentState = State.TESTDRIVE;
                    }
                    else
                    {
                        currentState = State.WELCOME;
                    }
                    break;
                case State.CHANGEAPPOINTMENT:
                    var message = GetAppointments();
                    if (inputMessage == "1")
                    {
                        if (message.Count == 1 && message.First().StartsWith("You do not"))
                        {
                            messages.Add(message.First());
                        }
                        else
                        {
                            messages.Add(CancelAllAppointments());
                        }
                        currentState = State.WELCOME;
                    }
                    else if(inputMessage == "2")
                    {                        
                        if(message.Count == 1 && message.First().StartsWith("You do not"))
                        {
                            messages.Add(message.First());
                            currentState = State.WELCOME;
                        }
                        else
                        {
                            messages.Add("Your Booked Appointments are here. Which one do you want to cancel. Select by Appointment Id: ");
                            messages.AddRange(message);
                        }
                    }
                    else
                    {
                        messages.Add(CancelAppointment(inputMessage));
                        currentState = State.WELCOME;
                    }
                    break;
                case State.ORDERINFO:
                    var messs = RetreiveOrderInfo(inputMessage);
                    messages.Add(messs);
                    if (!messs.StartsWith("Found"))
                    {
                        currentState = State.ORDERINFO;
                    }
                    else
                    {
                        currentState = State.WELCOME;
                    }                    
                    break;
            }

            foreach (var message in messages)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            return messages;
        }

        private string CancelAppointment(string appointmentId)
        {
            new Appointment().Cancel(appointmentId);
            return "Appointment Cancelled.";
        }

        private string CancelAllAppointments()
        {
            new Appointment().CancelAll();
            return "All appoinments Cancelled. To Go back to main menu, press any key";
        }

        private string BookTestDrive(string inputMessage)
        {
            var cars = GetCars();
            if(!cars.Any(c => c.Contains(inputMessage)))
            {
                return "Car Not Found! Enter Correct Car Id";
            }
            var testDrive = new TestDrive().Save(inputMessage);
            return $"Test Drive Booked for {DateTime.Now}. To Go back to main menu, press any key";
        }

        private string BookAppointment(string inputMessage)
        {
            var appointmentId = new Appointment().Save(inputMessage);
            return $"Appointment Booked for {DateTime.Now}. Your Appointment Id is: {appointmentId}. To Go back to main menu, press any key";
        }

        private string RetreiveOrderInfo(string inputMessage)
        {
            if(!inputMessage.Contains('@'))
            {
                return "Invalid Email. Try Again";
            }
            var order = new Order().Query(inputMessage);
            if(order == null)
            {
                return "Cannot find order with this email id. Try Again";
            }
            return $"Found your order! This order with orderid: {order.OrderId} is in {order.OrderStatus} state. To Go back to main menu, press any key";
            
        }

        private State GetStateFromOptionSelection(string input)
        {
            switch(input)
            {
                case "1":
                    return State.APPOINTMENT_BOOKING;
                case "2":
                    return State.TESTDRIVE;
                case "3":
                    return State.CHANGEAPPOINTMENT;
                case "4":
                    return State.ORDERINFO;
                default:
                    return State.WELCOME;
            }
        }

        private List<string> GetOptionSelection(string selectedOption)
        {
            var messages = new List<string>();
            switch (selectedOption)
            {
                case "1":
                    messages.Add("Sure! Who do you want to book an appointment with?");
                    messages.AddRange(GetSalesPersons());
                    break;
                case "2":
                    messages.Add("Sure! Which car do you want to test drive?");
                    messages.AddRange(GetCars());
                    break;
                case "3":
                    messages.Add("Sure! Which appoinment do you want to cancel?");
                    messages.Add("All Appoinments - To Select press 1");
                    messages.Add("Specific Appointment - To Select press 2");
                    break;
                case "4":
                    messages.Add("Sure! Please enter your email address?");
                    break;
            }
            return messages;
        }

        private List<string> GetSalesPersons()
        {
            var salesPersons = new SalesPerson().QueryAll();
            List<string> messages = salesPersons.Select(s => $"{s.SalesPersonName} (Sales Agent) - To Select press {s.SalesPersonId}").ToList();
            return messages;
        }

        private List<string> GetCars()
        {
            var cars = new Car().QueryAll();
            List<string> messages = cars.Select(c => $"{c.ModelName} - To Select press {c.CarId}").ToList();
            return messages;
        }

        private List<string> GetAppointments()
        {
            var appointments = new Appointment().QueryAll();
            if(appointments!=null && appointments.Any())
            {
                return appointments.Select(a => $"Appointment with AppointmentId: {a.AppointmentId} with SalesPersonId: {a.SalesPersonId} on {a.AppointmentDate} for {a.AppointmentType}").ToList();
            }
            return new List<string> { "You do not have any booked appointments" };
        }

    }
}
