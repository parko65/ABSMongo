using Contracts;
using Entities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service;
internal sealed class PLCService : IPLCService
{
    private readonly IOptionsMonitor<PLCConfigurationOptions> _plcOptions;
    private readonly ILoggerManager _logger;

    public PLCService(IOptionsMonitor<PLCConfigurationOptions> plcOptions, ILoggerManager logger)
    {
        _plcOptions = plcOptions;
        _logger = logger;
    }    
}