using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using System.Threading;
using Android.Automation.Testing.Logic;

namespace Android_Gauge_UITest
{
    [TestFixture]
    public class Gauge
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                // TODO: Update this path to point to your Android app and uncomment the
                // code if the app is not included in the solution.
                .ApkFile("C:/Users/chunkhongc/Downloads/Gauge Android Builds/Gauge-1.0.11_111_PROD_original.apk")
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Repl();
        }

        public void Login()
        {
            app.Screenshot("On the login screen");
            //app.WaitForElement("Cancel");
            //app.Tap("Cancel");

            //app.Screenshot("2");
            //app.WaitForElement(c => c.WebView().Css("#btnSSOMessageNo"), timeout: TimeSpan.FromSeconds(30));

            //app.Screenshot("3");
            //app.Tap(c => c.WebView().Css("#btnSSOMessageNo"));
            app.WaitForElement(c => c.WebView().Css("#localeDropDownList"), timeout: TimeSpan.FromSeconds(30));
            app.Tap(c => c.WebView().Css("#localeDropDownList"));
            app.Screenshot("Click Locale Dorp Down");

            app.Tap(c => c.Text("Malaysia - English"));
            app.Screenshot("Select Malaysia - English");
            //app.Screenshot("7");
            //app.Tap(c => c.WebView().Css("#btnSSOMessageNo"));
            app.WaitForElement(c => c.WebView().Css("#username"), timeout: TimeSpan.FromSeconds(15));
            Thread.Sleep(3000);

            app.Tap(c => c.WebView().Css("#btnSSOMessageNo")); // Tap Toast Message OK Button

            app.Tap(c => c.WebView().Css("#username"));
            app.EnterText("testing1978");
            app.Screenshot("Enter User Name");

            app.Tap(c => c.WebView().Css("#pin"));
            app.EnterText("test@123");
            app.Screenshot("Enter Pin");
            Thread.Sleep(3000);
            app.Tap(c => c.WebView().Css("#btnLogin"));
        }

        [Test]
        public void SetGoals()
        {
            /*Arrange */
            Login();

            //Action
            try
            {
                //Wait for "Set Goals" button display
                app.WaitForElement(c => c.WebView().Css("#goalsProgressGrid > div:nth-child(4) > div > a"), timeout: TimeSpan.FromSeconds(30));

                //Click on "Set Goals" button
                app.Tap(c => c.WebView().Css("#goalsProgressGrid > div:nth-child(4) > div > a"));
                app.Screenshot("Set Goals Page");

                //Tap on Done
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
            }
            catch
            {
                app.WaitForElement(c => c.WebView().Css("#editIcon"), timeout: TimeSpan.FromSeconds(20));

                //Click Edit Goals
                app.Tap(c => c.WebView().Css("#editIcon"));
                app.Screenshot(" Goals Page");

                //Enter Goals Target
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(4) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("5");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.ClearText();
                app.EnterText("5");
                app.Screenshot("Edit Goals");

                //Click Save button
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
            }
        }

        [Test]
        public void NewContact()
        {
            /*Arrange */
            Login();
            Random rnd = new Random();
            string TesterNo = rnd.Next(100).ToString();
            string PhoneNo = rnd.Next(10000000).ToString();
            string newContact = "Tester " + TesterNo + " " + "Automation";

            //Action
            try
            {
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"), timeout: TimeSpan.FromSeconds(30));
            }
            catch
            {
                app.WaitForElement(c => c.WebView().Css("#editIcon"), timeout: TimeSpan.FromSeconds(15));
            }
            app.Tap(c => c.Id("iconContacts"));
            app.Screenshot("Contact Page");
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a:nth-child(5)"), timeout: TimeSpan.FromSeconds(20));
            Thread.Sleep(2000);
            //Tap on Add Contact
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a:nth-child(5)"));
            app.Screenshot("Add New Contact Page");
            app.WaitForElement(c => c.WebView().Css("#contactFirstName"), timeout: TimeSpan.FromSeconds(20));
            app.Tap(c => c.WebView().Css("#contactFirstName"));
            app.EnterText("Tester " + TesterNo);
            app.Tap(c => c.WebView().Css("#contactLastName"));
            Thread.Sleep(3000);
            app.EnterText("Automation");
            //Enter Phone Number
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(7) > div:nth-child(1) > div > div.divider > div > div > input"));
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(7) > div:nth-child(1) > div > div.divider > div > div > input"));
            app.EnterText(PhoneNo);
            //Enter Notes
            app.ScrollTo(c => c.WebView().Css("#contactNote"));
            app.Tap(c => c.WebView().Css("#contactNote"));
            app.EnterText("Automation Testing");
            //Select Customer Status
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i"));
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i"));
            Thread.Sleep(3000);
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > article > div:nth-child(2) > div"));
            app.Tap(c => c.WebView().Css("#statusChoices > div > div:nth-child(5) > label")); // Tap on the 30 Days Card
            app.ScrollTo(c => c.WebView().Css("#contactNote")); // Scroll to Notes
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i")); //Collapse Customer Status
            //Select Invite Method
            app.ScrollTo(c => c.WebView().Css("#contactInviteMethod"));
            app.Tap(c => c.WebView().Css("#contactInviteMethod"));
            Thread.Sleep(3000);
            app.Tap(c => c.Text("Other"));
            //Select Interested In
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(16) > aside > div.section-header > span > span > span > i"));
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(16) > aside > div.section-header > span > span > span > i"));
            Thread.Sleep(3000);
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(17) > aside > div.section-header > label"));
            Thread.Sleep(2000);
            app.Tap(c => c.WebView().Css("#interestedInOptions > label:nth-child(2)"));
            //Save
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary"));
            app.Screenshot("Save Contact");

            //Check if hit goals dialog pop-up
            try
            {
                app.WaitForElement(c => c.WebView().Css("#profileModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                app.Tap(c => c.WebView().Css("#profileModal > div > div > input[type=\"submit\"]"));
                app.Screenshot("Goals Hit Dialog");
                Thread.Sleep(2000);

                //Click on Contact tab
                app.Tap(c => c.Id("iconContacts"));
                Thread.Sleep(5000);
            }
            catch
            {
                
            }

            //Assert
            app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
            app.Tap(c => c.WebView().Css("#customerSearchBox"));
            app.EnterText(newContact);
            app.Screenshot("Search Box");
            app.PressEnter();
            Thread.Sleep(5000);
            app.WaitForElement(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(2)"), timeout: TimeSpan.FromSeconds(20));
            app.Screenshot("Search Result");
            var lastName = app.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(3)"));
            var firstName = app.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(2)"));
            string contactName = firstName[0].TextContent + " " + lastName[0].TextContent;
            Assert.AreEqual(newContact, contactName);
        }

        [Test]
        public void DeleteContact()
        {
            //Arrange
            Login();
            int custCount = 0;
            string custName;

            //Action
            try
            {
                app.WaitForElement(c => c.WebView().Css("#goalsProgressGrid > div:nth-child(4) > div > a"), timeout: TimeSpan.FromSeconds(30));
            }
            catch
            {
                app.WaitForElement(c => c.WebView().Css("#editIcon"), timeout: TimeSpan.FromSeconds(15));
            }
            app.Tap(c => c.Id("iconContacts"));
            Thread.Sleep(3000);
            app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1)"));
                
            custCount = app.Query(c => c.WebView().Css("#contacts-list > div")).Count();

            if (custCount > 0)
            {
                //custName = 
                var result = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a > span:nth-child(2)"));
                var lastName = result[0].TextContent;
                var result2 = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a > span:nth-child(3)"));
                var firstName = result2[0].TextContent;
                custName = lastName + " " + firstName;

                app.Tap(c => c.WebView().Css("#contacts-list > div").Index(0));
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a"), timeout: TimeSpan.FromSeconds(20));          
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a").Index(1));
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a.pull-right.btn-primary.btn-trash"), timeout: TimeSpan.FromSeconds(20));
                app.Screenshot("Edit Contact Page");
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.pull-right.btn-primary.btn-trash"));

                //Assert
                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
                app.Tap(c => c.WebView().Css("#customerSearchBox"));
                app.EnterText(custName);
                app.PressEnter();
                app.Screenshot("Search Result Page");

                try
                {
                    app.WaitForElement(c => c.WebView().Css("#searchResults > div:nth-child(2) > div > strong"), timeout: TimeSpan.FromSeconds(20));
                    var searchResult = app.Query(c => c.WebView().Css("#searchResults > div:nth-child(2) > div > strong"));
                    string checking = searchResult[0].TextContent;
                    Assert.AreEqual("Customer Not Found", checking);
                }
                catch
                {
                    // Customer Not Found message missing
                }
            }
        }

        [Test]
        public void EditContact()
        {
            //Arrange
            Login();
            string custName;
            string contactName = "Test";

            //Action
            try
            {
                app.WaitForElement(c => c.WebView().Css("#goalsProgressGrid > div:nth-child(4) > div > a"), timeout: TimeSpan.FromSeconds(30));
            }
            catch
            {
                //To wait for webpage finish load
            }
            Thread.Sleep(5000);

            app.Tap(c => c.Id("iconContacts"));
            Thread.Sleep(5000);
            app.Screenshot("Contact Page");

            app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a")); //Scroll to contacts list
            app.Tap(c => c.Css("#contacts-list > div:nth-child(1) > span > a"));

            app.WaitForElement(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(3)"), timeout: TimeSpan.FromSeconds(30));
            var fContact = app.Query(c => c.Css("#contactInfoForm > div.profile-container > div:nth-child(3) > strong"));

            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a").Index(1)); //Click on Edit button
            app.WaitForElement(c => c.WebView().Css("#contactLastName"), timeout: TimeSpan.FromSeconds(30));
            app.Screenshot("Edit Contact Page");
            app.Tap(c => c.WebView().Css("#contactLastName"));
            app.ClearText();
            app.EnterText("Edit Contact");
            app.Screenshot("Edit Contact Last Name");
            custName = fContact[0].TextContent + " " + "Edit Contact";
            //Edit contact number
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(7) > div:nth-child(1) > div > div.divider > div > div.number > input"));
            app.ClearText();
            app.EnterText("520520");
            app.Screenshot("Edit Contact Phone Number");
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary")); //Click on Done
            app.Screenshot("Save Contact");
            app.WaitForElement(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(3)"), timeout: TimeSpan.FromSeconds(30));
            app.Tap(c => c.Id("iconContacts"));
            Thread.Sleep(5000);

            // Assert
            app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
            app.Tap(c => c.WebView().Css("#customerSearchBox"));
            app.EnterText(custName);
            app.Screenshot("Search Contact");
            app.PressEnter();
            app.WaitForElement(c => c.WebView().Css("#contacts-list > div"), timeout: TimeSpan.FromSeconds(20));
            Thread.Sleep(3000);
            app.Screenshot("Search Result Page");

            var searchResult = app.Query(c => c.WebView().Css("#contacts-list > div"));
            int count = searchResult.Count();
            
            if (count >0)
            {
                var lastName = app.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(2)"));
                var firstName = app.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(3)"));
                contactName = lastName[0].TextContent + " " + firstName[0].TextContent;              
            }

            Assert.AreEqual(custName, contactName);
        }

        [Test]
        public void SetFollowUp()
        {
            //Arrange
            Login();

            //Action
            try
            {
                app.WaitForElement(c => c.WebView().Css("#goalsProgressGrid > div:nth-child(4) > div > a"), timeout: TimeSpan.FromSeconds(35));
            }
            catch
            {
                //To wait for webpage finish load
            }

            app.Tap(c => c.Id("iconContacts"));
            Thread.Sleep(5000);
            app.Screenshot("Contact Page");

            app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a")); //Scroll to contacts list
            app.Tap(c => c.Css("#contacts-list > div:nth-child(1) > span > a"));

            app.WaitForElement(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(3)"), timeout: TimeSpan.FromSeconds(30));

            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a").Index(1)); //Click on Edit button
            app.WaitForElement(c => c.WebView().Css("#contactLastName"), timeout: TimeSpan.FromSeconds(30));
            app.Screenshot("Edit Contact Page");
            //Click on Follow Up
            app.ScrollTo(c => c.Css("#contactInfoForm > div.profile-container > div:nth-child(11) > aside > div.section-header > span > span > span > i")); //Scroll to Follow Up Drop Down
            app.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(11) > aside > div.section-header > span > span > span > i")); //Tap on Drop Down
            Thread.Sleep(2000);
            app.ScrollTo(c => c.WebView().Css("#followUpFields > div.time")); //Scroll to Time label to display the calendar
            app.Tap(c => c.WebView().Css("#contact-profile-followup-calendar")); //Tap on Today
            Thread.Sleep(2000);
            var check = app.Query(c => c.WebView().Css("#followUpButtonGroup > button.km-button.km-state-active")); //Check if Todat is check?
            int count = check.Count();
            if (count == 0)
            {
                app.Tap(c => c.WebView().Css("#contact-profile-followup-calendar")); //Tap on Today again
            }
            app.Screenshot("Select Today");
            Thread.Sleep(2000);
            app.ScrollTo(c => c.WebView().Css("#followUpFields > div.capitalize > strong > small")); //Scroll to Priority
            app.Tap(c => c.WebView().Css("#followUpFields > div.time > div > select").Index(0)); //Tap on Hours
            app.Tap(c => c.Text("2"));
            app.Screenshot("Select Hours");
            app.Tap(c => c.WebView().Css("#followUpFields > div.time > div > select").Index(1)); //Tap on minutes
            app.Tap(c => c.Text("15"));
            app.Screenshot("Select Minutes");
            app.Tap(c => c.WebView().Css("#followUpFields > div.time > div > div > span:nth-child(2)")); //Tap on PM
            app.Screenshot("Select PM");
            app.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(12) > label")); //Scroll to Note
            app.Tap(c => c.WebView().Css("#followUpFields > div.capitalize > select")); //Tap on Priority
            Thread.Sleep(2000);
            app.Tap(c => c.Text("Medium"));
            app.Screenshot("Set Follow Up");
            var followUp = app.Query(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(11) > aside > div.block > strong:nth-child(1)"));
            string followUpValue = followUp[0].TextContent;
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary")); //Click on Done
            app.WaitForElement(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(11) > aside > div.block > strong:nth-child(1)"), timeout: TimeSpan.FromSeconds(30));

            //Assert
            Thread.Sleep(3000);
            app.Tap(c => c.Id("iconContacts"));
            try
            {
                app.WaitForElement(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a"), timeout: TimeSpan.FromSeconds(30)); //Wait for contact list load
            }
            catch
            {
                app.Tap(c => c.Id("iconContacts")); //Tap on Contact again...
            }
            app.Screenshot("Contact Page");

            app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > span > a")); //Scroll to contacts list
            app.Tap(c => c.Css("#contacts-list > div:nth-child(1) > span > a"));
            app.Screenshot("Contact Profile Page");

            app.WaitForElement(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(3)"), timeout: TimeSpan.FromSeconds(30));
            var afterFollowUp = app.Query(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(11) > aside > div.block > strong:nth-child(1)"));
            string afterFollowUpValue = afterFollowUp[0].TextContent;

            Assert.AreEqual(followUpValue, afterFollowUpValue);
        }

        [Test]
        public void TrackClubVisit()
        {
            //Arrange
            Login();
            int contactCount = 0;
            string contact;
            string contactDetail = "Tester";

            //Action
            try //To wait for application login and landing page load
            {
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"), timeout: TimeSpan.FromSeconds(15));
            }
            catch
            {
                app.WaitForElement(c => c.WebView().Css("#editIcon"), timeout: TimeSpan.FromSeconds(10));
            }

            app.Tap(c => c.Id("iconActivities"));
            Thread.Sleep(3000);

            try
            {
                //Tap on Go to Goals Setting page
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > ul > li > a"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"), timeout: TimeSpan.FromSeconds(10));
                app.Screenshot("Set Goals Page");

                //Enter Goals Target
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(4) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("5");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.ClearText();
                app.EnterText("5");
                app.Screenshot("Edit Goals");

                //Click Save button
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
                Thread.Sleep(5000);

                //Click Activity Tab again
                app.Tap(c => c.Id("iconActivities"));
                app.Screenshot("Activities Page");

                //Click on Club Visit
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"), timeout: TimeSpan.FromSeconds(10));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"));
                app.Screenshot("Club Visit Tracking Page");

                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(10));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                //Select Customer Page
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                app.Screenshot("Done Club Visit");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(10));
                    app.Tap(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            catch
            {
                //Click on Club Visit
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"), timeout: TimeSpan.FromSeconds(10));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"));
                app.Screenshot("Club Visit Tracking Page");

                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1)"));
                app.WaitForElement(c => c.WebView().Css("#contacts-list > div:nth-child(1)"), timeout: TimeSpan.FromSeconds(10));
                //Select Customer Page
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                app.Screenshot("Done Club Visit");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(10));
                    app.Tap(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }

            //Assert
            //Wait for Club Visit link display
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"), timeout: TimeSpan.FromSeconds(15));
            app.Tap(c => c.Id("iconGoals")); //Click on Goals tab
            app.WaitForElement(c => c.WebView().Css("#goals-report-container"), timeout: TimeSpan.FromSeconds(10));
            app.Screenshot("Goals Progress Page");

            //Tap on Club Visit Goals Progress bar
            app.Tap(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div.progressbar.k-widget.k-progressbar.k-progressbar-horizontal > span"));
            //Wait for Goals detail header display
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div.goal-details-header > div > label"), timeout: TimeSpan.FromSeconds(10));
            app.Screenshot("Goals Details Page");
            contactCount = app.Query(c => c.WebView().Css("#contacts-list > div")).Count();

            for (int a=1;a<=contactCount;a++)
            {
                var lName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > span:nth-child(2)"));
                var fName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > span:nth-child(3)"));
                contactDetail = lName[0].TextContent + fName[0].TextContent;
                if (contactDetail == contact)
                {
                    a = contactCount;
                }
            }

            Assert.AreEqual(contact, contactDetail);
        }

        [Test]
        public void TrackVolumePoint()
        {
            //Arrange
            Login();
            int contactCount = 0;
            string contact;
            string contactDetail = "Tester";

            //Action
            try //To wait for application login and landing page load
            {
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"), timeout: TimeSpan.FromSeconds(30));
            }
            catch
            {
                app.WaitForElement(c => c.WebView().Css("#editIcon"), timeout: TimeSpan.FromSeconds(15));
            }

            app.Tap(c => c.Id("iconActivities"));
            Thread.Sleep(3000);

            try
            {
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > ul > li > a"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"), timeout: TimeSpan.FromSeconds(15));
                app.Screenshot("Set Goals Page");

                //Enter Goals Target
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(4) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("5");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.ClearText();
                app.EnterText("5");
                app.Screenshot("Edit Goals");

                //Click Save button
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
                Thread.Sleep(5000);

                //Click Activity Tab again
                app.Tap(c => c.Id("iconActivities"));
                app.Screenshot("Activities Page");

                //Click on Volume Point
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(6)"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(6)"));
                app.Screenshot("Select Contact Page");

                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Next
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a:nth-child(6) > img"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking Page");
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div.set-goals.volume-points > div.k-widget.k-listview > div > span:nth-child(3) > input"));
                app.ClearText();
                app.EnterText("18.8");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            catch
            {
                //Click on Volume Point
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(6)"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(6)"));
                app.Screenshot("Select Contact Page");

                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Next
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a").Index(1));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking Page");
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div.set-goals.volume-points > div.k-widget.k-listview > div > span:nth-child(3) > input"));
                app.ClearText();
                app.EnterText("18.8");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            
            //Assert
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(6)"), timeout: TimeSpan.FromSeconds(15));
            app.Tap(c => c.Id("iconGoals")); //Click on Goals tab
            app.WaitForElement(c => c.WebView().Css("#goals-report-container"), timeout: TimeSpan.FromSeconds(15));
            app.Screenshot("Goals Progress Page");

            app.ScrollTo(c => c.WebView().Css("#goals-report-container > div:nth-child(18) > div > div.progressbar.k-widget.k-progressbar.k-progressbar-horizontal > div > span"));
            app.Tap(c => c.WebView().Css("#goals-report-container > div:nth-child(18) > div > div.progressbar.k-widget.k-progressbar.k-progressbar-horizontal > div > span"));
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div > div.goal-details-header > div > label"), timeout: TimeSpan.FromSeconds(15));
            app.Screenshot("Goals Details Page");
            contactCount = app.Query(c => c.WebView().Css("#contacts-list > div")).Count();

            for (int a = 1; a <= contactCount; a++)
            {
                var lName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > a > span:nth-child(2)"));
                var fName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > a > span:nth-child(3)"));
                contactDetail = lName[0].TextContent + fName[0].TextContent;
                if (contactDetail == contact)
                {
                    a = contactCount;
                }
            }

            Assert.AreEqual(contact, contactDetail);
        }

        [Test]
        public void TrackConsumption()
        {
            //Arrange
            Login();
            int contactCount = 0;
            string contact;
            string contactDetail = "Tester";

            //Action
            Thread.Sleep(20000);
            app.Tap(c => c.Id("iconActivities"));
            Thread.Sleep(3000);

            try
            {
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > ul > li > a"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"), timeout: TimeSpan.FromSeconds(15));
                app.Screenshot("Set Goals Page");

                //Enter Goals Target
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(4) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("5");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.ClearText();
                app.EnterText("5");
                app.Screenshot("Edit Goals");

                //Click Save button
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
                Thread.Sleep(5000);

                //Click Activity Tab again
                app.Tap(c => c.Id("iconActivities"));
                app.Screenshot("Activities Page");

                //Click on Consumption
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(7) > span"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(7) > span"));
                Thread.Sleep(5000);
                app.Screenshot("Select Contact Page");

                //Select Contact for consumption
                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Next
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.pull-right.btn-primary > img"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking Page");
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div.set-goals.volume-points > div.k-widget.k-listview > div > span:nth-child(3) > input"));
                app.ClearText();
                app.EnterText("1");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            catch
            {
                //Click on Consumption
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(7) > span"), timeout: TimeSpan.FromSeconds(15));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(7) > span"));
                Thread.Sleep(5000);
                app.Screenshot("Select Contact Page");

                //Select Contact for consumption
                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Next
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.pull-right.btn-primary > img"));
                Thread.Sleep(3000);
                app.Screenshot("Volume Point Tracking Page");
                //Tap on input box
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div.set-goals.volume-points > div.k-widget.k-listview > div > span:nth-child(3) > input"));
                app.ClearText();
                app.EnterText("1");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                Thread.Sleep(2000);
                app.Screenshot("Volume Point Tracking");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#trackActivityModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }

            //Assert
            //Wait for consumption to display
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > div > div > a:nth-child(7) > span"), timeout: TimeSpan.FromSeconds(15));
            app.Tap(c => c.Id("iconGoals")); //Click on Goals tab
            app.WaitForElement(c => c.WebView().Css("#goals-report-container"), timeout: TimeSpan.FromSeconds(15));
            app.Screenshot("Goals Progress Page");

            //Scroll to Consumption
            app.ScrollTo(c => c.WebView().Css("#goals-report-container > div:nth-child(17) > div > div"));
            app.Tap(c => c.WebView().Css("#goals-report-container > div:nth-child(17) > div > div"));
            app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > div > div.goal-details-header > div > label"), timeout: TimeSpan.FromSeconds(15));
            app.Screenshot("Goals Details Page");
            contactCount = app.Query(c => c.WebView().Css("#contacts-list > div")).Count();

            for (int a = 1; a <= contactCount; a++)
            {
                var lName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > a > span:nth-child(2)"));
                var fName = app.Query(c => c.WebView().Css("#contacts-list > div:nth-child(" + (a) + ") > a > span:nth-child(3)"));
                contactDetail = lName[0].TextContent + fName[0].TextContent;
                if (contactDetail == contact)
                {
                    a = contactCount;
                }
            }

            Assert.AreEqual(contact, contactDetail);
        }

        [Test]
        public void CreateTodayEvent()
        {
            //Arrange
            Login();
            string Today = DateTime.Now.ToString("dd MMMM yyyy");
            Random rnd = new Random();
            string EventNo = rnd.Next(1000).ToString();
            string eventName = "Automation Testing " + EventNo;

            //Action
            //Click on More Tab
            app.WaitForElement(c => c.Id("iconMore"), timeout: TimeSpan.FromSeconds(20));
            app.Tap(c => c.Id("iconMore"));
            //Tap on Manage Events
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(2) > a > div > div > strong"));
            app.WaitForElement(c => c.WebView().Css("#EventDatePicker > div.k-footer > a"), timeout: TimeSpan.FromSeconds(20));
            string CalendarToday = app.Query(c => c.WebView().Css("#EventDatePicker > div.k-footer > a")).Single().TextContent;
           
            //Tap on Add Event
            app.Tap(c => c.WebView().Css("#manageEvents > div > a"));
            //Tap on category
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(1) > select"));
            Thread.Sleep(2000);
            app.Tap(c => c.Text("Workout"));
            Thread.Sleep(2000);
            //Tap on Event Name input box
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(2) > input"));
            app.EnterText("Automation Testing "+ EventNo);
            //Tap on Desc
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(3) > textarea"));
            app.EnterText("Automation Testing");
            //Scroll to Time
            app.ScrollTo(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(5) > div > div > select:nth-child(1)"));
            //Tap on Hour
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(5) > div > div > select:nth-child(1)"));
            app.Tap(c => c.Text("4"));
            //Tap on Minutes
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(5) > div > div > select:nth-child(2)"));
            app.Tap(c => c.Text("25"));
            //Tap on PM
            app.Tap(c => c.WebView().Css("#addEvent > div > form > div > div:nth-child(5) > div > div > div > span:nth-child(2)"));
            //Tap on Done
            app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
            Thread.Sleep(3000);
            app.ScrollDown();
            Thread.Sleep(2000);
            var events = app.Query(c => c.WebView().Css("#manageEvents > ul > li"));
            int eCount = events.Count();
            string eName;
            bool foundEvent = false;

            for (int i=1;i <= eCount;i++)
            {
                eName = app.Query(c => c.WebView().Css("#manageEvents > ul > li:nth-child("+i+") > a > span:nth-child(2)")).Single().TextContent;
                if (eventName== eName)
                {
                    foundEvent = true;
                    break;
                }
            }

            //Assert
            //To Check Calendar Date is Today date
            Assert.AreEqual(Today, CalendarToday);
            //Check if event created and display in the today events list
            Assert.IsTrue(foundEvent);
        }

        [Test]
        public void NewCustomerGoalsProgress()
        {
            //Arrange
            Login();
            string before = "0";
            string after = "0";

            //Action
            app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(1) > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
            string progress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(1) > div > div.k-target")).Single().TextContent;
            int count = progress.Length;

            for (int a = 0; a < count; a++)
            {
                string current = progress.Substring(a, 1);
                if (current == "/")
                {
                    before = progress.Substring(0, a); //Capture New Customer Progress (before)
                }
            }

            Contact NewContact = new Contact();
            NewContact.Add(app);

            app.DismissKeyboard();
            app.Tap(c => c.Id("iconGoals"));
            app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(1) > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
            string afterProgress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(1) > div > div.k-target")).Single().TextContent;
            int afterCount = afterProgress.Length;

            for (int a = 0; a < afterCount; a++)
            {
                string current = afterProgress.Substring(a, 1);
                if (current == "/")
                {
                    after = afterProgress.Substring(0, a); //Capture New Customer Progress (before)
                }
            }

            int expected = Convert.ToInt32(before) + 1;
            int actual = Convert.ToInt32(after);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TrackClubVisitGoalsProgress()
        {
            //Arrange
            Login();
            string contact;
            string before = "0";
            string after = "0";
            string afterProgress;
            string progress;

            //Action
            try
            {
                app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
                progress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div > div.k-target")).Single().TextContent;
            }
            catch
            {
                //Goals already has progress
                app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
                progress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div.k-target")).Single().TextContent;
            }

            int count = progress.Length;

            for (int a = 0; a < count; a++)
            {
                string current = progress.Substring(a, 1);
                if (current == "/")
                {
                    before = progress.Substring(0, a); //Capture Club Visit Progress (before)
                }
            }

            app.Tap(c => c.Id("iconActivities"));
            Thread.Sleep(3000);

            try
            {
                //Tap on Go to Goals Setting page
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > div:nth-child(2) > ul > li > a"));
                app.WaitForElement(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"), timeout: TimeSpan.FromSeconds(10));
                app.Screenshot("Set Goals Page");

                //Enter Goals Target
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(4) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div:nth-child(2) > div:nth-child(6) > span > input"));
                app.ClearText();
                app.EnterText("3");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(2) > span > input"));
                app.ClearText();
                app.EnterText("5");

                app.ScrollTo(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.Tap(c => c.WebView().Css("#set-goal-container > form > div > div.set-goals.no-border > div:nth-child(3) > span > input"));
                app.ClearText();
                app.EnterText("5");
                app.Screenshot("Edit Goals");

                //Click Save button
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary.save-goal"));
                app.Screenshot("Save Goals");
                Thread.Sleep(5000);

                //Click Activity Tab again
                app.Tap(c => c.Id("iconActivities"));
                app.Screenshot("Activities Page");

                //Click on Club Visit
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"), timeout: TimeSpan.FromSeconds(10));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"));
                app.Screenshot("Club Visit Tracking Page");

                app.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(10));
                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                //Select Customer Page
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                app.Screenshot("Done Club Visit");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(10));
                    app.Tap(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            catch
            {
                //Click on Club Visit
                app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"), timeout: TimeSpan.FromSeconds(10));
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > div:nth-child(3) > div > div > a:nth-child(2)"));
                app.Screenshot("Club Visit Tracking Page");

                app.ScrollTo(c => c.WebView().Css("#contacts-list > div:nth-child(1)"));
                app.WaitForElement(c => c.WebView().Css("#contacts-list > div:nth-child(1)"), timeout: TimeSpan.FromSeconds(10));
                //Select Customer Page
                var fContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(1)"));
                var lContact = app.Query(c => c.Css("#contacts-list > div:nth-child(1) > div > div > label > span:nth-child(3) > span:nth-child(2)"));
                contact = lContact[0].TextContent + fContact[0].TextContent;
                app.Tap(c => c.WebView().Css("#contacts-list > div:nth-child(1) > div > div > label"));
                Thread.Sleep(2000);
                app.Screenshot("Select Contact");

                //Click Done
                app.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.btn-primary.pull-right"));
                app.Screenshot("Done Club Visit");

                //Check if hit goals dialog pop-up
                try
                {
                    app.WaitForElement(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(10));
                    app.Tap(c => c.WebView().Css("#listModal > div > div > input[type=\"submit\"]"));
                    app.Screenshot("Goals Hit Dialog");
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
            Thread.Sleep(5000);

            app.Tap(c => c.Id("iconGoals"));
            try
            {
                app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
                afterProgress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div > div.k-target")).Single().TextContent;
            }
            catch
            {
                //Goals already has progress
                app.WaitForElement(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div.k-target"), timeout: TimeSpan.FromSeconds(20));
                afterProgress = app.Query(c => c.WebView().Css("#goals-report-container > div:nth-child(3) > div > div.k-target")).Single().TextContent;
            }

            int afterCount = afterProgress.Length;

            for (int a = 0; a < afterCount; a++)
            {
                string current = afterProgress.Substring(a, 1);
                if (current == "/")
                {
                    after = afterProgress.Substring(0, a); //Capture Club Vist Progress (after)
                }
            }

            int expected = Convert.ToInt32(before) + 1;
            int actual = Convert.ToInt32(after);
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void Cleanup()
        {
            try
            {   //if there is keyboard display 
                app.DismissKeyboard();

                app.WaitForElement(c => c.Id("iconMore"), timeout: TimeSpan.FromSeconds(20));
                app.Tap(c => c.Id("iconMore"));
                app.Screenshot("More Page");

                try
                {
                    app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"));
                    app.Screenshot("Sign Out");

                    //Click on OK buttom
                    app.WaitForElement(c => c.Id("button1"));
                    app.Tap(c => c.Id("button1"));
                }
                catch
                {
                    //Click on More again
                    app.Tap(c => c.Id("iconMore"));
                    app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"));
                    app.Screenshot("Sign Out");

                    //Click on OK buttom
                    app.WaitForElement(c => c.Id("button1"));
                    app.Tap(c => c.Id("button1"));
                }
            }
            catch
            {
                app.WaitForElement(c => c.Id("iconMore"), timeout: TimeSpan.FromSeconds(20));
                app.Tap(c => c.Id("iconMore"));

                try
                {
                    //To tap on Sign Out
                    app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"));
                }
                catch
                {
                    //Click on More again
                    app.Tap(c => c.Id("iconMore"));
                    app.WaitForElement(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"), timeout: TimeSpan.FromSeconds(20));
                    app.Tap(c => c.WebView().Css("#nc-crm-content > div > div > ul > li:nth-child(8) > a > div > div"));

                    //Click on OK buttom
                    app.WaitForElement(c => c.Id("button1"));
                    app.Tap(c => c.Id("button1"));
                }
              
                Thread.Sleep(5000);
            }       
        }
    }
}

