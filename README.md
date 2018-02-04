# MVC-Test-End-To-End

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


**Class Model**
## Some Sweet Theory 