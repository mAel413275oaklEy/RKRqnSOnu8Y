// 代码生成时间: 2025-09-24 00:31:35
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileBackupSyncTool
{
    public class FileBackupSyncTool
    {
        private readonly string sourceDirectory;
        private readonly string backupDirectory;

        // Constructor to initialize the source and backup directories.
        public FileBackupSyncTool(string sourceDirectory, string backupDirectory)
        {
            this.sourceDirectory = sourceDirectory ?? throw new ArgumentNullException(nameof(sourceDirectory));
            this.backupDirectory = backupDirectory ?? throw new ArgumentNullException(nameof(backupDirectory));
        }

        // Method to backup files from the source directory to the backup directory.
        public async Task<bool> BackupFilesAsync()
        {
            try
            {
                // Check if the source and backup directories exist.
                if (!Directory.Exists(sourceDirectory))
                {
                    throw new DirectoryNotFoundException($"The source directory {sourceDirectory} was not found.");
                }

                // Create the backup directory if it does not exist.
                Directory.CreateDirectory(backupDirectory);

                // Copy files from the source to the backup directory.
                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    var fileName = Path.GetFileName(file);
                    var backupFile = Path.Combine(backupDirectory, fileName);

                    // Check if the file already exists in the backup directory.
                    if (File.Exists(backupFile))
                    {
                        // If the file exists, compare the file times and only overwrite if the source is newer.
                        if (File.GetLastWriteTime(file) > File.GetLastWriteTime(backupFile))
                        {
                            File.Copy(file, backupFile, true);
                        }
                    }
                    else
                    {
                        // If the file does not exist, copy it to the backup directory.
                        File.Copy(file, backupFile);
                    }
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Log the exception and return false to indicate failure.
                Console.WriteLine($"An error occurred during backup: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}
