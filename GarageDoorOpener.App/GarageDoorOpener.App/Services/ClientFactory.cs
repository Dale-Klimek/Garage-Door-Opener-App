using GarageDoorOpener.App.Interfaces;
using GarageDoorOpener.Shared.Protos;

using Grpc.Net.Client;

using System.Collections.Concurrent;

namespace GarageDoorOpener.App.Services;

internal class ClientFactory : IClientFactory, IDisposable, IAsyncDisposable
{
    private readonly IHttpClientFactory _factory;
    private readonly IPreferenceService _preferenceService;
    private readonly ConcurrentDictionary<string, GrpcChannel> _list = new();
    private bool disposedValue;

    public ClientFactory(IHttpClientFactory factory, IPreferenceService preferenceService)
    {
        _factory = factory;
        _preferenceService = preferenceService;
    }

    public GarageDoor.GarageDoorClient CreateClient()
    {
        var url = _preferenceService.GetBackendServerUrl();

        var channel = _list.GetOrAdd(url, (url) =>
        {
            var options = new GrpcChannelOptions() { HttpClient = _factory.CreateClient() };
            return GrpcChannel.ForAddress(url, options);
        });
        return new GarageDoor.GarageDoorClient(channel);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                foreach (var channel in _list.Values)
                {
                    channel.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ChannelFactory()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var channel in _list.Values)
        {
            await channel.ShutdownAsync();
        }
    }
}
