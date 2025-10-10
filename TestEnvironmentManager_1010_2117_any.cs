// 代码生成时间: 2025-10-10 21:17:50
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEnvironmentApp
{
    // Define an exception class for test environment related errors.
    public class TestEnvironmentException : Exception
    {
        public TestEnvironmentException(string message) : base(message)
        {
        }
    }

    // Define the TestEnvironment class to represent a test environment.
    public class TestEnvironment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    // Define the TestEnvironmentManager class to manage test environments.
    public class TestEnvironmentManager
    {
        private List<TestEnvironment> environments = new List<TestEnvironment>();

        // Adds a new test environment to the list.
        public void AddEnvironment(TestEnvironment environment)
        {
            if (environment == null)
            {
                throw new TestEnvironmentException("Cannot add a null environment.");
            }

            if (environments.Any(env => env.Name.Equals(environment.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TestEnvironmentException(\$"Environment with name '{environment.Name}' already exists.");
            }

            environments.Add(environment);
        }

        // Removes a test environment from the list.
        public bool RemoveEnvironment(string environmentName)
        {
            var environment = environments.FirstOrDefault(env => env.Name.Equals(environmentName, StringComparison.OrdinalIgnoreCase));
            if (environment != null)
            {
                environments.Remove(environment);
                return true;
            }
            return false;
        }

        // Lists all test environments.
        public IEnumerable<TestEnvironment> ListEnvironments()
        {
            return environments;
        }

        // Checks if a test environment with the given name exists.
        public bool EnvironmentExists(string environmentName)
        {
            return environments.Any(env => env.Name.Equals(environmentName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
