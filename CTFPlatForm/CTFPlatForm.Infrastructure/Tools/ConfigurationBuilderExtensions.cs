using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Infrastructure.Tools
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEmbeddedJsonFile(this IConfigurationBuilder builder, Assembly assembly, string filePathInAssembly)
        {
            var resourceName = $"{assembly.GetName().Name}.{filePathInAssembly.Replace("/", ".")}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"Embedded file '{resourceName}' not found in assembly '{assembly.FullName}'.");

                builder.AddJsonStream(stream);
            }

            return builder;
        }
    }
}
