using FluentAssertions;
using OOPsReview;

namespace TDDUnitTesting
{
    public class Person_Should
    {
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

        //Change_FirstName
        //Change_LastName
        //Change_FullName
    }
}