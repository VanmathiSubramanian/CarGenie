using Xunit;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        [Fact]
        public void TestThatTheBotDisplaysWelcomeMessageOnInitialHello()
        {
            var session = new Session("12345");
            var message = session.OnMessage("hello")[0];
            Assert.Equal("Welcome to CarGenie Bot!", message);
        }

        [Fact]
        public void TestThatBotDisplaysOptionsToSelectWithTheWelcomeMessage()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            Assert.Equal(6, messages.Count);
            var messageLine1 = messages[1];
            Assert.Equal("I can help you today with the below options. Please select one.", messageLine1);

            var messageLine2 = messages[2];
            Assert.Equal("Book an appointment - To select press 1", messageLine2);

            var messageLine3 = messages[3];
            Assert.Equal("Book a test drive  - To select press 2", messageLine3);

            var messageLine4 = messages[4];
            Assert.Equal("Change an appointment  - To select press 3", messageLine4);

            var messageLine5 = messages[5];
            Assert.Equal("Information about your order  - To select press 4", messageLine5);
        }

        [Fact]
        public void TestThatWelcomeMessagingPerformanceIsLessThan10000MilliSeconds()
        {
            var startTime = DateTime.Now;
            var session = new Session("12345");
            string input = session.OnMessage("hello")[0];
            var finished = DateTime.Now;
            var elapsed = (finished - startTime).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + elapsed);
            Assert.True(elapsed < 10000);
        }

        [Fact]
        public void TestThatSelectingOption1InTheWelcomeMessageDisplaysMessagesForBookingAnAppointment()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("1");

            Assert.Equal(5, messages.Count);

            var messageLine1 = messages[0];
            Assert.Equal("Sure! Who do you want to book an appointment with?", messageLine1);

            var messageLine2 = messages[1];
            Assert.Equal("Vanmathi Subramanian (Sales Agent) - To Select press 1", messageLine2);

            var messageLine3 = messages[2];
            Assert.Equal("Preethi Manjuvanda Ganesh (Sales Agent) - To Select press 2", messageLine3);

            var messageLine4 = messages[3];
            Assert.Equal("Tuney Mathew (Sales Agent) - To Select press 3", messageLine4);
        }

        [Fact]
        public void TestThatOption1SelectionInWelcomePagePerformanceIsLessThan10000MilliSeconds()
        {
            var startTime = DateTime.Now;
            var session = new Session("12345");
            var input = session.OnMessage("hello");
            input = session.OnMessage("1");
            var finished = DateTime.Now;
            var elapsed = (finished - startTime).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + elapsed);
            Assert.True(elapsed < 10000000);
        }

        [Fact]
        public void TestThatSelectingOption2InTheWelcomeMessageDisplaysMessagesForBookingATestDrive()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("2");

            Assert.Equal(4, messages.Count);

            var messageLine1 = messages[0];
            Assert.Equal("Sure! Which car do you want to test drive?", messageLine1);

            var messageLine2 = messages[1];
            Assert.Equal("Toyota Corolla - To Select press 1", messageLine2);

            var messageLine3 = messages[2];
            Assert.Equal("Toyota Prius - To Select press 2", messageLine3);

            var messageLine4 = messages[3];
            Assert.Equal("Toyota Highlander - To Select press 3", messageLine4);
        }

        [Fact]
        public void TestThatOption2SelectionInWelcomePagePerformanceIsLessThan10000MilliSeconds()
        {
            var startTime = DateTime.Now;
            var session = new Session("12345");
            var input = session.OnMessage("hello");
            input = session.OnMessage("2");
            var finished = DateTime.Now;
            var elapsed = (finished - startTime).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + elapsed);
            Assert.True(elapsed < 10000000);
        }

        [Fact]
        public void TestThatSelectingOption3InTheWelcomeMessageDisplaysMessagesForChangingAnAppointment()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("3");

            Assert.Equal(3, messages.Count);

            var messageLine1 = messages[0];
            Assert.Equal("Sure! Which appoinment do you want to cancel?", messageLine1);

            var messageLine2 = messages[1];
            Assert.Equal("All Appoinments - To Select press 1", messageLine2);

            var messageLine3 = messages[2];
            Assert.Equal("Specific Appointment - To Select press 2", messageLine3);
        }

        [Fact]
        public void TestThatOption3SelectionInWelcomePagePerformanceIsLessThan10000MilliSeconds()
        {
            var startTime = DateTime.Now;
            var session = new Session("12345");
            var input = session.OnMessage("hello");
            input = session.OnMessage("3");
            var finished = DateTime.Now;
            var elapsed = (finished - startTime).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + elapsed);
            Assert.True(elapsed < 10000);
        }

        [Fact]
        public void TestThatSelectingOption4InTheWelcomeMessageDisplaysMessagesForInquiringAboutOrder()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("4");

            Assert.Equal(1, messages.Count);

            var messageLine1 = messages[0];
            Assert.Equal("Sure! Please enter your email address?", messageLine1);
        }

        [Fact]
        public void TestThatOption4SelectionInWelcomePagePerformanceIsLessThan10000MilliSeconds()
        {
            var startTime = DateTime.Now;
            var session = new Session("12345");
            var input = session.OnMessage("hello");
            input = session.OnMessage("4");
            var finished = DateTime.Now;
            var elapsed = (finished - startTime).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + elapsed);
            Assert.True(elapsed < 10000);
        }

        [Fact]
        public void TestThatEnteringInvalidOptionOnWelcomeMessageDisplaysError()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("7");

            Assert.True(messages.Count == 1);
            var message= messages.First();
            Assert.Equal("Unsupported Option. Press any key to go back to the welcome page", message);
        }

        [Fact]
        public void TestThatEnteringInvalidEmailThrowsErrorDuringOrderInquiry()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("4");
            messages = session.OnMessage("123");

            Assert.True(messages.Count == 1);
            var message = messages.First();
            Assert.Equal("Invalid Email. Try Again", message);
        }

        [Fact]
        public void TestSelectingOption3LoadsRightMessagesInWelcomeStage()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("3");
            Assert.True(messages.Count() == 3);
            var message = messages[0];
            Assert.Equal("Sure! Which appoinment do you want to cancel?", message);
            message = messages[1];
            Assert.Equal("All Appoinments - To Select press 1", message);
            message = messages[2];
            Assert.Equal("Specific Appointment - To Select press 2", message);
        }

        [Fact]
        public void TestSelectingOption4LoadsRightMessagesInWelcomeStage()
        {
            var session = new Session("12345");
            var messages = session.OnMessage("hello");
            messages = session.OnMessage("4");
            Assert.True(messages.Count() == 1);
            var message = messages[0];
            Assert.Equal("Sure! Please enter your email address?", message);
        }
    }
}
