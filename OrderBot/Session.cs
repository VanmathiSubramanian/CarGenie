using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOME, OPTIONSELECTION
        }

        private State currentState = State.WELCOME;

        private Order order;

        public Session(string sPhone)
        {
            order = new Order();
            order.Phone = sPhone;
        }

        public List<string> OnMessage(string inputMessage)
        {
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
                    messages.AddRange(GetOptionSelection(inputMessage));
                    break;
            }

            foreach (var message in messages)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            return messages;
        }

        private List<string> GetOptionSelection(string selectedOption)
        {
            var messages = new List<string>();
            switch (selectedOption)
            {
                case "1":
                    messages.Add("Sure! Who do you want to book an appointment with?");
                    messages.Add("Vanmathi S (Sales Agent) - To Select press 1");
                    messages.Add("Preethi MG (Sales Agent) - To Select press 2");
                    messages.Add("Tuney M (Sales Agent) - To Select press 3");
                    break;
                case "2":
                    messages.Add("Sure! Which car do you want to test drive?");
                    messages.Add("Toyota Corolla - To Select press 1");
                    messages.Add("Toyota Highlander - To Select press 2");
                    messages.Add("Toyota Prius - To Select press 3");
                    break;
                case "3":
                    messages.Add("Sure! Which appoinment do you want to cancel?");
                    messages.Add("All Appoinments - To Select press 1");
                    messages.Add("Specific Appointment - To Select press 2");
                    break;
                case "4":
                    messages.Add("Sure! Please enter your order number?");
                    break;
            }
            return messages;
        }

    }
}
