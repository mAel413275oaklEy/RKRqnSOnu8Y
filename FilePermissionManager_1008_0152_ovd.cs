// 代码生成时间: 2025-10-08 01:52:20
using System;
using System.IO;
using System.Security.AccessControl;

namespace FilePermissionManager
{
    public class FilePermissionManager
    {
        // 检查文件权限
        public bool CheckFilePermission(string filePath, FileSystemRights rights)
        {
            try
            {
                var fileSecurity = File.GetAccessControl(filePath);
                var accessRule = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));

                foreach (FileSystemAccessRule rule in accessRule)
                {
                    if (rule.FileSystemRights.HasFlag(rights))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("权限不足，无法检查: " + filePath);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("检查文件权限时发生错误: " + ex.Message);
                return false;
            }
        }

        // 设置文件权限
        public void SetFilePermission(string filePath, FileSystemRights rights, AccessControlType controlType)
        {
            try
            {
                var fileSecurity = File.GetAccessControl(filePath);
                var accessRule = new FileSystemAccessRule(
                    "Everyone",
                    rights,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    controlType);

                fileSecurity.AddAccessRule(accessRule);
                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("权限不足，无法设置: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("设置文件权限时发生错误: " + ex.Message);
            }
        }
    }
}
