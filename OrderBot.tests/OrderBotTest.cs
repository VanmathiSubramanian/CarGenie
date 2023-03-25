using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

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

            Assert.Equal(4, messages.Count);

            var messageLine1 = messages[0];
            Assert.Equal("Sure! Who do you want to book an appointment with?", messageLine1);

            var messageLine2 = messages[1];
            Assert.Equal("Vanmathi S (Sales Agent) - To Select press 1", messageLine2);

            var messageLine3 = messages[2];
            Assert.Equal("Preethi MG (Sales Agent) - To Select press 2", messageLine3);

            var messageLine4 = messages[3];
            Assert.Equal("Tuney M (Sales Agent) - To Select press 3", messageLine4);
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
            Assert.True(elapsed < 10000);
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
            Assert.Equal("Toyota Highlander - To Select press 2", messageLine3);

            var messageLine4 = messages[3];
            Assert.Equal("Toyota Prius - To Select press 3", messageLine4);
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
            Assert.True(elapsed < 10000);
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
            Assert.Equal("Sure! Please enter your order number?", messageLine1);
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
    }
}
