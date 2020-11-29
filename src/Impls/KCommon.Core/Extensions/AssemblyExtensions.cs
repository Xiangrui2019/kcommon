using KCommon.Core.Validations;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取程序集的文件版本
        /// </summary>
        public static string GetFileVersion(this Assembly assembly)
        {
            if (!new NotNull().IsValid(assembly))
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info.FileVersion;
        }

        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        public static string GetProductVersion(this Assembly assembly)
        {
            if (!new NotNull().IsValid(assembly))
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info.ProductVersion;
        }

        /// <summary>
        /// 获取CLI版本号
        /// </summary>
        public static string GetCliVersion()
        {
            string[] dllNames =
            {
                "Microsoft.EntityFrameworkCore",
                "Microsoft.Extensions.Configuration.Binder",
                "Microsoft.Extensions.DependencyInjection",
                "Microsoft.Extensions.DependencyInjection.Abstractions",
                "Microsoft.Extensions.Configuration.Abstractions"
            };
            CompilationLibrary lib = null;
            foreach (string dllName in dllNames)
            {
                lib = DependencyContext.Default.CompileLibraries.FirstOrDefault(m => m.Name == dllName);
                if (lib != null)
                {
                    break;
                }
            }
            string cliVersion = lib?.Version;
            return cliVersion;
        }
    }
}
