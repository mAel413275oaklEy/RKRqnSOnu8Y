// 代码生成时间: 2025-10-06 01:59:21
 * Description:
 * This class provides a simple network traffic monitoring feature using C# and Blazor framework.
 * It periodically checks the network traffic and updates the UI with the latest data.
 *
 * Author: Your Name
 * Date: Current Date
 */
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace YourNamespace
{
    public partial class NetworkTrafficMonitor
    {
        [Parameter]
        public EventCallback<int> OnTrafficUpdated { get; set; }

        private long sentBytes = 0;
        private long receivedBytes = 0;

        private bool isMonitoring = false;

        // This method starts the network traffic monitoring
        public async Task StartMonitoring()
        {
            if (isMonitoring)
            {
                Console.WriteLine("Monitoring is already running.");
                return;
            }

            isMonitoring = true;
            await InvokeAsync(StateHasChanged);

            while (isMonitoring)
            {
                try
                {
                    // Get network traffic data
                    var sent = NetworkInterface.GetIPv4Statistics().BytesSent;
                    var received = NetworkInterface.GetIPv4Statistics().BytesReceived;

                    // Calculate the difference in bytes since the last check
                    var newSentBytes = sent - sentBytes;
                    var newReceivedBytes = received - receivedBytes;

                    // Update the sent and received bytes
                    sentBytes = sent;
                    receivedBytes = received;

                    // Invoke the OnTrafficUpdated event with the traffic data
                    await OnTrafficUpdated.InvokeAsync(newSentBytes + newReceivedBytes);

                    // Wait for a few seconds before the next check
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error monitoring network traffic: {ex.Message}");
                }
            }
        }

        // This method stops the network traffic monitoring
        public void StopMonitoring()
        {
            isMonitoring = false;
        }

        // This method is called when the component is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopMonitoring();
            }
            base.Dispose(disposing);
        }
    }
}