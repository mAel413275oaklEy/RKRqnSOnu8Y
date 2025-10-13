// 代码生成时间: 2025-10-13 21:14:32
using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components;

namespace FileIntegrityChecker
{
    /// <summary>
    /// 文件完整性校验器组件
    /// </summary>
    public class FileIntegrityChecker : ComponentBase
    {
        /// <summary>
        /// 文件完整性校验结果
        /// </summary>
        [Parameter]
        public EventCallback<string> OnFileChecked { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string filePath;

        /// <summary>
        /// 检查文件完整性
        /// </summary>
        public async Task CheckFileIntegrity()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("文件路径不能为空");
            }

            try
            {
                // 计算文件的哈希值
                string fileHash = CalculateFileHash(filePath);
                OnFileChecked.InvokeAsync($"文件完整性校验成功：{fileHash}");
            }
            catch (Exception ex)
            {
                // 处理异常情况
                OnFileChecked.InvokeAsync($"文件完整性校验失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件哈希值</returns>
        private string CalculateFileHash(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}