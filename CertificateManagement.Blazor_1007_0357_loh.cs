// 代码生成时间: 2025-10-07 03:57:21
 * Author: [Your Name]
 * Date: [Date]
 */

using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace CertificateManagement.Blazor
{
    // Component for managing SSL/TLS certificates
    public partial class CertificateManagementComponent
    {
        // File path to store certificates
        private string certificatePath = "./certificates/";

        // Current SSL/TLS certificate data
        private X509Certificate2 certificate;

        [Parameter]
        public EventCallback OnCertificateChanged { get; set; }

        // Method to load a certificate from a file
        private async Task LoadCertificate(string filePath)
        {
            try
            {
                certificate = new X509Certificate2(filePath);
                StateHasChanged();
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Error loading certificate: {ex.Message}");
            }
        }

        // Method to save a certificate to a file
        private async Task SaveCertificate(string filePath)
        {
            try
            {
                File.WriteAllBytes(filePath, certificate.RawData);
                await OnCertificateChanged.InvokeAsync(null);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error saving certificate: {ex.Message}");
            }
        }

        // Method to list all certificates in the directory
        private async Task<List<string>> ListCertificates()
        {
            try
            {
                var files = Directory.GetFiles(certificatePath, "*.crt");
                return files.ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error listing certificates: {ex.Message}");
                return new List<string>();
            }
        }

        // Method called when the user wants to load a certificate
        private async Task OnLoadCertificateClicked()
        {
            var files = await ListCertificates();
            foreach (var file in files)
            {
                await LoadCertificate(file);
                break; // Load the first valid certificate
            }
        }

        // Method called when the user wants to save a certificate
        private async Task OnSaveCertificateClicked()
        {
            var filePath = Path.Combine(certificatePath, "new_certificate.crt");
            await SaveCertificate(filePath);
        }
    }
}
