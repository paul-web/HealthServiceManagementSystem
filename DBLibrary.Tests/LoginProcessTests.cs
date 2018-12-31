using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.Tests
{
    public class LoginProcessTests
    {
        [Fact]
        // test accepted user credentials
        public void ValidateUserInput_StringsShouldVerify()
        {
            // Arrange 
            // declare and instantiate class to test
            LoginProcess loginProcess = new LoginProcess();
            // set boolean of expected result
            bool expected = true;

            // Act
            // set boolean for the actual result
            bool actual = loginProcess.valdateEmailPasswordInput("testmail@clinic.com", "passwordTest");

            // Assert
            // compare results
            Assert.Equal(expected, actual);
        }

        [Fact]
        // test non accepted email credentials
        public void ValidateUserInput_EmailShouldFail()
        {
            // Arrange 
            LoginProcess loginProcess = new LoginProcess();
            bool expected = false;

            // Act
            // email is not in a valid format and should fail as expected
            bool actual = loginProcess.valdateEmailPasswordInput("testmailclinic.com", "passwordTest");

            // Assert
            // compare results
            Assert.Equal(expected, actual);
        }

        [Fact]
        // test non accepted password credentials
        public void ValidateUserInput_PasswordShouldFail()
        {
            // Arrange 
            LoginProcess loginProcess = new LoginProcess();
            bool expected = false;

            // Act
            // password is not a minimum of 5 characters and should fail as expected
            bool actual = loginProcess.valdateEmailPasswordInput("testmailclinic.com", "pass");

            // Assert
            // compare results
            Assert.Equal(expected, actual);
        }
    }
}
