# Unit Tests for JwtContractReCounterApiClient

## Overview
This test suite provides comprehensive unit tests for the `JwtContractReCounterApiClient` class in the BackendCarcassContracts project.

## Test Project Details
- **Project Name**: BackendCarcassContracts.Tests
- **Framework**: .NET 10.0
- **Testing Framework**: xUnit 2.9.3
- **Mocking Framework**: Moq 4.20.72
- **Test SDK**: Microsoft.NET.Test.Sdk 17.14.1

## Test Coverage

The test suite includes 11 unit tests covering all public methods of the `JwtContractReCounterApiClient` class:

### Constructor Tests
1. **Constructor_WithValidParameters_CreatesInstance**
   - Verifies that the client can be successfully instantiated with valid parameters

### IsCurrentUserValid Method Tests
2. **IsCurrentUserValid_WithValidToken_ReturnsNone**
   - Tests successful token validation returns no errors
   - Mocks HTTP GET request with OK status

3. **IsCurrentUserValid_WithInvalidToken_ReturnsErrors**
   - Tests invalid token validation returns error array
   - Mocks HTTP GET request with Unauthorized status

4. **IsCurrentUserValid_WithCancellation_ThrowsTaskCanceledException**
   - Tests cancellation token behavior
   - Verifies proper cancellation handling

### Login Method Tests
5. **Login_WithValidCredentials_ReturnsLoginResponse**
   - Tests successful login with valid credentials
   - Verifies LoginResponse properties match expected values
   - Mocks HTTP POST request with OK status

6. **Login_WithInvalidCredentials_ReturnsErrors**
   - Tests failed login with invalid credentials
   - Verifies error array is returned
   - Mocks HTTP POST request with Unauthorized status

7. **Login_WithCancellation_ThrowsTaskCanceledException**
   - Tests cancellation token behavior during login
   - Verifies proper cancellation handling

8. **Login_SerializesRequestCorrectly**
   - Tests that LoginRequest is properly serialized to JSON
   - Captures and validates the HTTP request content

### SetToken Method Tests
9. **SetToken_WithValidToken_UpdatesAccessToken**
   - Tests that SetToken updates the access token property
   - Verifies the new token value is set correctly

10. **SetToken_WithNullToken_SetsAccessTokenToNull**
    - Tests that SetToken can clear the token by setting it to null
    - Verifies null token handling

11. **SetToken_CalledMultipleTimes_UpdatesTokenEachTime**
    - Tests that SetToken can be called multiple times
    - Verifies each call updates the token correctly

## Test Architecture

### Test Setup
- Uses constructor injection for test dependencies
- Creates mocks for:
  - `ILogger<JwtContractReCounterApiClient>`
  - `IHttpClientFactory`
  - `HttpMessageHandler`
- Implements `IDisposable` for proper HttpClient cleanup

### Testable Wrapper Class
The tests use a `TestableJwtContractReCounterApiClient` wrapper class that:
- Inherits from `JwtContractReCounterApiClient`
- Exposes protected members for testing
- Provides `GetAccessToken()` method to verify token state

### Mocking Strategy
- Uses Moq's Protected() method to mock HttpMessageHandler.SendAsync
- Verifies HTTP method, URI, headers, and request content
- Returns appropriate HttpResponseMessage with status codes and content

## Running the Tests

### Command Line
```bash
# Run all tests
dotnet test BackendCarcassContracts.Tests\BackendCarcassContracts.Tests.csproj

# Run with detailed output
dotnet test BackendCarcassContracts.Tests\BackendCarcassContracts.Tests.csproj --logger "console;verbosity=detailed"

# Run specific test
dotnet test --filter "FullyQualifiedName~Login_WithValidCredentials"
```

### Visual Studio
1. Open Test Explorer (Test > Test Explorer)
2. Build the solution
3. All tests will appear in the Test Explorer
4. Click "Run All" or run individual tests

## Test Results
✅ All 11 tests passing
- 0 failed
- 0 skipped
- Average execution time: ~73ms

## Notes
- Tests use .NET 10.0 with backward-compatible packages (some target .NET 6.0)
- The `CopyLocalLockFileAssemblies` property ensures all dependencies are copied to output
- TaskCanceledException is used instead of OperationCanceledException as it's the actual exception thrown by HttpClient
- Tests follow AAA (Arrange-Act-Assert) pattern for clarity
- Each test is independent and can run in isolation

## Dependencies Added
The following packages were added to support testing:
- xunit (2.9.3)
- xunit.runner.visualstudio (3.1.4)
- Moq (4.20.72)
- Microsoft.NET.Test.Sdk (17.14.1)

These were also added to `Directory.Packages.props` for central package management.
