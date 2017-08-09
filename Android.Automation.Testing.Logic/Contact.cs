using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.UITest.Android;

namespace Android.Automation.Testing.Logic
{
    public class Contact
    {
        public void Add(AndroidApp appUse)
        {
            //Arrange
            Random rnd = new Random();
            string TesterNo = rnd.Next(1000).ToString();
            string PhoneNo = rnd.Next(100000).ToString();
            string newContact = "Tester " + TesterNo + " " + "Automation Testing";

            try
            {
                appUse.WaitForElement(c => c.WebView().Css("#goalsProgressGrid"), timeout: TimeSpan.FromSeconds(20));
            }
            catch
            {
                // Wait for the page to load
            }

            //Action 
            Thread.Sleep(2000);
            appUse.Tap(c => c.Id("iconContacts"));
            Thread.Sleep(3000);
            appUse.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a:nth-child(5) > img"));

            appUse.WaitForElement(c => c.WebView().Css("#contactFirstName"), timeout: TimeSpan.FromSeconds(30)); //Wait for element visible
            appUse.Tap(c => c.WebView().Css("#contactFirstName"));
            appUse.EnterText("Tester " + TesterNo);
            appUse.Tap(c => c.WebView().Css("#contactLastName"));
            appUse.EnterText("Automation Testing");
            appUse.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(7) > div:nth-child(1) > div > div.divider > div > div.number > input"));
            appUse.EnterText(PhoneNo);
            appUse.ScrollTo(c => c.WebView().Css("#contactNote"));
            appUse.Tap(c => c.WebView().Css("#contactNote"));
            appUse.EnterText("Automation Test");
            //Select Customer Status
            appUse.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i"));
            appUse.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i"));
            Thread.Sleep(3000);
            appUse.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > article > div:nth-child(2) > div"));
            appUse.Tap(c => c.WebView().Css("#statusChoices > div > div:nth-child(5) > label")); // Tap on the 30 Days Card
            appUse.ScrollTo(c => c.WebView().Css("#contactNote")); // Scroll to Notes
            appUse.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(13) > aside > span > span > span > i")); //Collapse Customer Status
            //Select Invite Method
            appUse.ScrollTo(c => c.WebView().Css("#contactInviteMethod"));
            appUse.Tap(c => c.WebView().Css("#contactInviteMethod"));
            Thread.Sleep(3000);
            appUse.Tap(c => c.Text("Other"));
            //Select Interested In
            appUse.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(16) > aside > div.section-header > span > span > span > i"));
            appUse.Tap(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(16) > aside > div.section-header > span > span > span > i"));
            Thread.Sleep(3000);
            appUse.ScrollTo(c => c.WebView().Css("#contactInfoForm > div.profile-container > div:nth-child(17) > aside > div.section-header > label"));
            Thread.Sleep(2000);
            appUse.Tap(c => c.WebView().Css("#interestedInOptions > label:nth-child(2)"));
            //Save
            appUse.Tap(c => c.WebView().Css("#nc-crm-content > div > header > a.right.pull-right.btn-primary"));
            appUse.Screenshot("Save Contact");

            //Check if hit goals dialog pop-up
            try
            {
                appUse.WaitForElement(c => c.WebView().Css("#profileModal > div > div > input[type=\"submit\"]"), timeout: TimeSpan.FromSeconds(20));
                appUse.Tap(c => c.WebView().Css("#profileModal > div > div > input[type=\"submit\"]"));
                appUse.Screenshot("Goals Hit Dialog");
                Thread.Sleep(2000);

                //Click on Contact tab
                appUse.Tap(c => c.Id("iconContacts"));
                Thread.Sleep(5000);
            }
            catch
            {

            }

            //Assert
            appUse.WaitForElement(c => c.WebView().Css("#customerSearchBox"), timeout: TimeSpan.FromSeconds(20));
            appUse.Tap(c => c.WebView().Css("#customerSearchBox"));
            appUse.EnterText(newContact);
            appUse.Screenshot("Search Box");
            appUse.PressEnter();
            Thread.Sleep(5000);
            appUse.WaitForElement(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(2)"), timeout: TimeSpan.FromSeconds(20));
            appUse.Screenshot("Search Result");
            var lastName = appUse.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(3)"));
            var firstName = appUse.Query(c => c.WebView().Css("#contacts-list > div > span > a > span:nth-child(2)"));
            string contactName = firstName[0].TextContent + " " + lastName[0].TextContent;
            Assert.AreEqual(newContact, contactName);
            Thread.Sleep(2000);
        }
    }
}
