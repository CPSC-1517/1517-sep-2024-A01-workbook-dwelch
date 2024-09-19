using FluentAssertions;
using OOPsReview;

namespace TDDUnitTesting
{
    public class Person_Should
    {
        #region constructors
        //valid data testing

        //a Fact unit test executes once
        //without the annotation the method is NOT considered a unit test
        [Fact]
        public void Create_Instance_Using_Default_Constructor()
        {
            //Arrange (setup of need code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;

            //Act ( this is the action that is under testing)
            // sut: subject under test
            Person sut = new Person();

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }

        [Fact]
        public void Create_Instance_Using_Greedy_Construtor_Without_Address_And_Employments()
        {
            //Arrange (setup of need code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;

            //Act ( this is the action that is under testing)
            // sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, null, null);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }

        [Fact]
        public void Create_Instance_Using_Greedy_Construtor_With_Address_And_Without_Employments()
        {
            //Arrange (setup of need code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;
            ResidentAddress expectedAddress = new ResidentAddress(12, "Maple St", "Edmonton", "AB", "T6Y7U8");

            //Act ( this is the action that is under testing)
            // sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, expectedAddress, null);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }
        [Fact]
        public void Create_Instance_Using_Greedy_Construtor_With_Address_And_Employments()
        {
            //Arrange (setup of need code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 2;
            ResidentAddress expectedAddress = new ResidentAddress(12, "Maple St", "Edmonton", "AB", "T6Y7U8");
            Employment one = new Employment("Team Member", SupervisoryLevel.TeamMember,
                                DateTime.Parse("2013/10/23"), 6.5);
            Employment two = new Employment("Team Lead", SupervisoryLevel.TeamLeader,
                                DateTime.Parse("2020/04/13"), 4.4);
            List<Employment> expectedEmployments = new List<Employment>();
            expectedEmployments.Add(one);
            expectedEmployments.Add(two);

            //Act ( this is the action that is under testing)
            // sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, expectedAddress, expectedEmployments);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
            //enough though the count is 2, did the employments actually get properly loaded
            //one way to set your collection to be the proper instances AND in the expected order
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmployments);
        }

        //invalid data testing
        //a [Theory] test will execute once for each [InlineData] notation
        //[InlineData] holds the test value for the iteration of the test
        //the method needs a parameter to receive the data from the [InlineData] annotation
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Creating_Instance_Using_Greedy_Construtor_With_Bad_First_Name(string firstname)
        {
            //Arrange (setup of need code for doing the test)
            //there is not setup for this test
           

            //Act ( this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown
            
            Action action = () => new Person(firstname, "Behold", null, null);

            //Assert (check the results of the act against expected values)
            action.Should().Throw<ArgumentNullException>();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Creating_Instance_Using_Greedy_Construtor_With_Bad_Last_Name(string lastname)
        {
            //Arrange (setup of need code for doing the test)
            //there is not setup for this test


            //Act ( this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown

            Action action = () => new Person("Lowand", lastname, null, null);

            //Assert (check the results of the act against expected values)
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region properties
        //valid data
        [Fact]
        public void Change_First_Name_Via_Property()
        {
            //Arrange (setup of needed code for doing the test)
            string expectedFirstName = "Bob";

            Person sut = new Person("don", "welch", null, null);

            //Act ( this is the action that is under testing)
            // sut: subject under test
            sut.FirstName = "Bob";

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
          
        }
      
        [Fact]
        public void Change_Last_Name_Via_Property()
        {
            //Arrange (setup of needed code for doing the test)
            string expectedLastName = "Behold";

            Person sut = new Person("don", "welch", null, null);

            //Act ( this is the action that is under testing)
            // sut: subject under test
            sut.LastName = "Behold";

            //Assert (check the results of the act against expected values)
            sut.LastName.Should().Be(expectedLastName);

        }
        //invalid data tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Changing_FirstName_With_Bad_First_Name(string firstname)
        {
            //Arrange (setup of need code for doing the test)
            Person sut = new Person("Lowand", "Behold", null, null);


            //Act ( this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown

            Action action = () => sut.FirstName = firstname;

            //Assert (check the results of the act against expected values)
            action.Should().Throw<ArgumentNullException>();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Changing_LastName_With_Bad_Last_Name(string lastname)
        {
            //Arrange (setup of need code for doing the test)
            Person sut = new Person("Lowand", "Behold", null, null);


            //Act ( this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown

            Action action = () => sut.LastName = lastname;

            //Assert (check the results of the act against expected values)
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region methods
        //Change_FullName
        [Fact]
        public void Change_Peron_First_And_Last_Name_Via_Method()
        {
            //Arrange (setup of needed code for doing the test)
            string expectedLastName = "Behold";
            string expectedFirstName = "Lowand";

            Person sut = new Person("don", "welch", null, null);

            //Act ( this is the action that is under testing)
            // sut: subject under test
            sut.ChangeFullName("Lowand", "Behold");

            //Assert (check the results of the act against expected values)
            sut.LastName.Should().Be(expectedLastName);
            sut.FirstName.Should().Be(expectedFirstName);

        }
        [Theory]
        [InlineData(null, "Behold")]
        [InlineData("", "Behold")]
        [InlineData("   ", "Behold")]
        [InlineData("Lowand", null)]
        [InlineData("Lowand", "")]
        [InlineData("Lowand", "   ")]
        public void Throw_Exception_Changing_Both_Names_With_Bad_Name_Values_Using_Method(string firstname, string lastname)
        {
            //Arrange (setup of need code for doing the test)
            Person sut = new Person("Lowand", "Behold", null, null);


            //Act ( this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown

            Action action = () => sut.ChangeFullName(firstname, lastname);

            //Assert (check the results of the act against expected values)
            action.Should().Throw<ArgumentNullException>();
        }
        
        #endregion
    }
}