// 代码生成时间: 2025-10-10 02:43:24
// ServiceDiscoveryRegistry.cs
// This class represents a service discovery and registration system.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServiceDiscovery
{
    // Define a class for service registration and discovery.
    public class ServiceDiscoveryRegistry
    {
        private readonly Dictionary<string, string> serviceRegistry = new Dictionary<string, string>();

        // Register a service with a unique key and its location URL.
        public async Task<bool> RegisterServiceAsync(string serviceName, string serviceUrl)
        {
            try
            {
                // Check if the service is already registered.
                if (serviceRegistry.ContainsKey(serviceName))
                {
                    throw new ArgumentException($"Service with name {serviceName} is already registered.");
                }

                // Add the service to the registry.
                serviceRegistry.Add(serviceName, serviceUrl);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception and return false.
                Console.WriteLine($"Error registering service: {ex.Message}");
                return false;
            }
        }

        // Discover a service by its name.
        public async Task<string> DiscoverServiceAsync(string serviceName)
        {
            try
            {
                // Check if the service is registered.
                if (serviceRegistry.TryGetValue(serviceName, out string serviceUrl))
                {
                    return serviceUrl;
                }
                else
                {
                    throw new KeyNotFoundException($"Service with name {serviceName} not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return null.
                Console.WriteLine($"Error discovering service: {ex.Message}");
                return null;
            }
        }
    }
}
