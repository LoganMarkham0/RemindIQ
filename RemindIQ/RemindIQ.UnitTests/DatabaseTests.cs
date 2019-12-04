using System;
using Xunit;
using RemindIQ.Services;
using RemindIQ.Models;
using System.IO;
using System.Threading;

namespace RemindIQ.UnitTests
{
    public class DatabaseTests
    {
        [Fact]
        public async void InsertReminder()
        {
            bool noErrors;

            // Database object
            DatabaseHelper databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));

            // Create valid reminder.
            Reminder reminder = new Reminder
            {
                Id = 1,
                Latitude = 38.8977,
                Longitude = 77.0365,
                DestinationAddress = "White House",
                Name = "White House Reminder",
                Range = 10.0,
                Status = 0,
                DistanceToDestination = 20.0,
                HasBeenNotified = false,
                Notes = null,
            };

            // test insertion into Database
            try
            {
                // Act
                await databaseHelper.AddReminderAsync(reminder);
                noErrors = true; // did not throw exception when adding reminder
            }
            catch (Exception)
            {
                // threw exception, error occured
                noErrors = false;
            }

            // Assert
            Assert.True(noErrors);
        }


        [Fact]
        public async void ReadReminder()
        {
            bool returnedCorrectly = false;
            const string NAME = "White House Reminder";
            const int ID = 2;

            // Database object
            DatabaseHelper databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));

            // Create valid reminder.
            Reminder reminder = new Reminder
            {
                Id = ID,
                Latitude = 38.8977,
                Longitude = 77.0365,
                DestinationAddress = "White House",
                Name = NAME,
                Range = 10.0,
                Status = 0,
                DistanceToDestination = 20.0,
                HasBeenNotified = false,
                Notes = null,
            };

            // Add reminder
            await databaseHelper.AddReminderAsync(reminder);

            // ensure there is enough time for reminder to be added to database before querying
            Thread.Sleep(1000);

            // Act
            Reminder returnedReminder = await databaseHelper.GetReminderAsync(ID);

            // Assert
            if (returnedReminder.Name == NAME)
            {
                returnedCorrectly = true;
            }

            Assert.True(returnedCorrectly);
        }

        [Fact]
        public async void DeleteReminder()
        {
            bool noErrors = true;
            const int ID = 3;

            // Database object
            DatabaseHelper databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));

            // Create valid reminder.
            Reminder reminder = new Reminder
            {
                Id = ID,
                Latitude = 38.8977,
                Longitude = 77.0365,
                DestinationAddress = "White House",
                Name = "White House Reminder",
                Range = 10.0,
                Status = 0,
                DistanceToDestination = 20.0,
                HasBeenNotified = false,
                Notes = null,
            };

            // Add reminder
            await databaseHelper.AddReminderAsync(reminder);

            // Act
            try
            {
                await databaseHelper.DeleteReminderAsync(reminder);
            }
            catch (Exception)
            {
                noErrors = false;   // if exception occurs when trying to delete the test fails
            }

            // Assert
            Assert.True(noErrors);
        }

        [Fact]
        public async void UpdateReminder()
        {
            bool updated = false;
            const string NAME = "White House Reminder";
            const string UPDATEDNAME = "Updated Reminder";
            const int ID = 1;

            // Database object
            DatabaseHelper databaseHelper = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemindIQ.db3"));

            // Create valid reminder.
            Reminder reminder = new Reminder
            {
                Id = ID,
                Latitude = 38.8977,
                Longitude = 77.0365,
                DestinationAddress = "White House",
                Name = NAME,
                Range = 10.0,
                Status = 0,
                DistanceToDestination = 20.0,
                HasBeenNotified = false,
                Notes = null,
            };

            // Add reminder
            await databaseHelper.AddReminderAsync(reminder);

            // Alter original reminder
            reminder.Name = UPDATEDNAME;

            // Update Reminder
            try
            {
                // Act
                await databaseHelper.UpdateReminderAsync(reminder);
                updated = true;
            }
            catch (Exception)
            {
                updated = false;
            }

            Assert.True(updated);
        }
    }
}
