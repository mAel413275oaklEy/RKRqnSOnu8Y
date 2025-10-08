// 代码生成时间: 2025-10-09 02:32:20
// 文件压缩解压工具 - FileDecompressor.cs
using System;
using System.IO;
using System.IO.Compression; // 用于文件压缩和解压

namespace FileDecompressorApp
{
    // 提供文件压缩和解压功能的工具类
    public static class FileDecompressor
    {
        /// <summary>
        /// 解压指定的压缩文件到目标目录。
        /// </summary>
        /// <param name="filePath">压缩文件的路径</param>
        /// <param name="destinationPath">解压后的目标目录路径</param>
        /// <returns>解压操作是否成功</returns>
        public static bool DecompressFile(string filePath, string destinationPath)
        {
            try
            {
                // 确保目标目录存在，如果不存在，则创建
                Directory.CreateDirectory(destinationPath);

                // 使用ZIP文件解压缩方法
                ZipFile.ExtractToDirectory(filePath, destinationPath);

                // 如果没有异常发生，则解压成功
                return true;
            }
            catch (Exception ex)
            {
                // 处理异常，记录错误日志
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 压缩指定的文件或目录到ZIP文件。
        /// </summary>
        /// <param name="sourcePath">要压缩的文件或目录的路径</param>
        /// <param name="zipFilePath">生成的ZIP文件的路径</param>
        /// <returns>压缩操作是否成功</returns>
        public static bool CompressFile(string sourcePath, string zipFilePath)
        {
            try
            {
                // 使用ZIP文件压缩方法
                ZipFile.CreateFromDirectory(sourcePath, zipFilePath);

                // 如果没有异常发生，则压缩成功
                return true;
            }
            catch (Exception ex)
            {
                // 处理异常，记录错误日志
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
