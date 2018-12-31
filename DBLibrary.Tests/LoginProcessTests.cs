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
        // declare and instantiate class to test
        LoginProcess loginProcess = new LoginProcess();

        [Theory]
        [InlineData ("validmail@clinic.com", "password", true)] // test valid input
        [InlineData("mail@clinic.com", "gt5ooj", true)] // test valid input
        [InlineData("", "", false)] // test empty input textboxes
        [InlineData("validmail@clinic.com", "pa", false)] // test invaliud password length
        [InlineData("validmail@clinic.com", "22ff", false)] // test invalid password length
        [InlineData("validmail@clinic.com", "", false)] // test invalid empty password field
        [InlineData("invalidmailformat.com", "password", false)] // test invalid email format (no @ symbol)
        [InlineData("invalidmail@formatcom", "password", false)] // test invalid email format (no .)
        [InlineData("validmail@format.com", "1password", false)] // test invalid email format beginning with number
        [InlineData("validmail@format.com", "77password", false)] // test invalid email format beginning with number
        [InlineData("validmail@format.com", "9092pass", false)] // test invalid email format beginning with number
        [InlineData("validmail@mail.com", "apasswordtestthatisgreaterthanthirtycharactersinlength", false)] // test invalid password length > 30 chars
        [InlineData("validmail@mail.com", "anotherpasswordtestthatisgreaterthanthirtycharactersinlength", false)] // test invalid password length > 30 chars




        // test accepted user credentials
        public void ValidateUserInput_StringsShouldVerify(string email, string password, bool expected)
        {
            // Arrange 

            // Act
            // set boolean for the actual result
            bool actual = loginProcess.valdateEmailPasswordInput(email,password);

            // Assert
            // compare results
            Assert.Equal(expected, actual);
        }

        [Fact]
        // test empty user input
        public void ValidateUserInput_EmptyStringsShouldFail()
        {
            // Arrange 
            // set boolean of expected result
            bool expected = false;

            // Act
            // set boolean for the actual result
            bool actual = loginProcess.valdateEmailPasswordInput("", "");

            // Assert
            // compare results
            Assert.Equal(expected, actual);
        }


        [Fact]
        // test non accepted email credentials
        public void ValidateUserInput_EmailShouldFail()
        {
            // Arrange 
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
