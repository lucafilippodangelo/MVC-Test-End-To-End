# MVC Unit and Integration Test Project (NUnit, moq, Teamcity) 

In this test project I used "NUnit" + "moq", below a summary of main topics

- Piramis of Tests
- Unit Tests Isolation Styles
- Testing The Model
  - UT
  - Integration test
- Testing Controllers
- Continuous Integration
- Team City

## Some Introduction Theory

**Why Automated Testing?**

- Quality software:
  - Automated tests 
  - Code Reviews
  - Pair Programming
  - Motivated Development Team
  - Well Understood Requirement


**The automated test pyramid**

- top: fewer test, longer execution(maybe based on web services), brittle because based on UI element, Automating the user interface(hitting buttons) 

- middle: involve multiple less related classes, may include databases, files

- bottom: more test, more stable, shorter execution time. UNIT, usually single class, deep, isolated, focused, test all the valid inputs(NUnit TestCases)

**Unit Test Isolation Styles**:

- CLASSIC: use "fake" or "real"(where easy to use) versions of the collaborators.
- MOCKIST: always use fake collaborators(class from wich the class that we are testing depend) to build the input for the test.


## The Project Step By Step

**Setting of the connection with the DB**

- DbContext.cs
       
       ```
      public class BankingSitedDb : DbContext
      {
        public BankingSitedDb() : base("name=CustomConnectionAutomatedTestingProject") {  }        
        public DbSet<LoanApplication> LoanApplications { get; set; }        
      }
      ```

- Connection String

      <add name="CustomConnectionAutomatedTestingProject" connectionString="Data Source=LUCA; Initial Catalog=DbAutomatedTestingProject; Integrated Security=False;User ID=sa;Password=Luca111q;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" providerName="System.Data.SqlClient" />

- DB Name "DbAutomatedTestingProject"

1 - install entity framework from nuget
2 - run "Enable-Migrations"
3 - run "Add-Migration aName"
4 - run "Update-Database"

**Testing the MODEL**

- Installation of the RUNNER "NUnit" 
  - [TestFixture] - attribute for a class that contain tests. the test runner is able to recognize that.
  - [Test] this attribute mark a public method as a test and the test runner know to execute that as a test.

- INSTALL: "Tools" --> "Extensions and Updates" --> "Nunit Test Adapter"

- Writing of some UT
  - //LD STEP1
  - add the project "BankingSite.UnitTest" and then in this project we have to install the testing framework "Nunit".

- Writing unit test with CLASSIC collaborator (of a class)
  - we are testing the behaviour of methods inside classes, to do that we have to create an instance of this class by a CLASSIC approach.
  - //LD STEP3 it's when we pass in input a concrete instance, like we do with dependency injection 
  - //LD STEP4 we create the test class: "LoanApplicationScorerTests" 
  - //LD STEP5 and then we implement the example:

- Writing unit test with MOCKIST STYLE collaborator (of a class)
  - INSTALLATION of a MOCKING framework --> "Moq" by nuget package.
  - //LD STEP6 let's see a first example
  - //LD STEP7 here I create a fake instance to pass to "LoanApplicationScorer", but remember that I need to specify the "OBJECT":

- Writing Integration Tests (where there is INTERACTION WITH DATABASE)
  - we are creating a TEST for the INTERACTION WITH THE DATABASE by a repository class.
  - //LD STEP8 I have a setting class "TestFixtureLifecycle"  where we do some setting each time we execute a test
  - //LD STEP9 now I create the TEST class

      
**Testing Controllers**

- Writing a controller test manually
  - //LD STEP10 test of controller returning the default view
  - //LD STEP11 
  - //LD STEP12 Fluent MVC Testing
  - //LD STEP13 test the redirection to a specific view under conditions


## Running Tests on TeamCity Continuous Integration Server

**Continuous Integration**

Definition: dedicated server that build together all the code changes from different delelopers and run the tests in the build.

So the steps are:
- Compiles?
- Tests Pass?
- Code Metrics(for instance: unit tests coverage)
- Feedback to the team

Smart way to find bugs sooner

**TEAMCITY**

it works with "C#"+"MSBuild"+"NUnit"+ Nuget code coverage tool

The parts we have to have are:
 - "Source Control" local or git
 - dedicated "Teamcity Server" to run the team city service. 

This service has to be connected with the "Source Control" and get the code.
 - the "Team City Build Agent" make the build
 - the Structure of TeamCity is:

      1 - project(BankingSite Project)
        - A PROJECT HAS MANY -> 
      2 - Build Configuration(Unit Test) ->  Build Configuration(Integration Test)
        - EACH BUILD CONFIGURATION HAS MANY TEST STEP -> 
      3 - Step()

**Build PIPELINES**

Definition: series of phases I want to happen when I check in code. My pipeline in TeamCity will be:

      project(BankingSite Project)
            A PROJECT HAS MANY phases of"Build configuration" -> 
            Phase One->  
                  EACH BUILD CONFIGURATION HAS MANY TEST STEP -> 
                  Build Solution
                  Unit Test
            Phase Two->  
                  EACH BUILD CONFIGURATION HAS MANY TEST STEP -> 
                  Integration Test


**TEAMCITY SETUP - (phase one) CREATE TRIGGER TO MAKE A BUILD ONCE CODE COMMITTED**

1 - create the **project** in teamcity. 

2 - Under the project I have to configure the "build configuration". in this case I created the build configuration -> "Build Solution + Execute Unit Tests"

3 - Then on the left menu I have to click in "version Control Settings" inside the build configuration -> "Build Solution After Code Committed (phase one)", I have to specify the "Version Control Settings", in this case I added one from GIT by using the url "https://github.com/lucafilippodangelo/MVC-Testing-End-To-End"
So now I can get our source code.

4 - Then I have to add a STEP, on the left menu of this "phase one" click on "Build Steps". Here I have to specify the solution I want build, in this case "BankingSite.sln"

4 - Then I have to automatize,  on the left menu for the "phase one" I click on **triggers** in order to execute the build automatically without click on the RUN button.

**TEAMCITY SETUP - (phase two) EXECUTE UNIT TESTS**

- I will create a new build configuration under the same project "there are images attached abiut the configuration"

- I have to add a step for unit tests, remember that if I WANT RUN A BUILD AFTER THAT THE PREVIOUS IS FINICHED, I have to set in the left menu the "dependencies" -> "Add new snapshot dependency"

- after I add a "trigger" -> "finish build trigger" -> and as build I specify the "phase one" 

- so now if "phase one" run successfully, then "phase two" is trigged


