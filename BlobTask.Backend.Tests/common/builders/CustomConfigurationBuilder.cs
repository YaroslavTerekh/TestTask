using BlobTask.Backend.Tests.common.constants;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobTask.Backend.Tests.common.builders;

public static class CustomConfigurationBuilder
{
    public static IConfigurationRoot BuildConfiguration()
    {
        var myConfiguration = new Dictionary<string, string>
        {
            {"AzureStorage:ConnectionString", ConfigurationConstants.ConnectionString},
            {"AzureStorage:ContainerName", ConfigurationConstants.ContainerName},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        return configuration;
    }
}
