using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Helper
{
    public class CacheProvider
    {

        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _innerCache;

        /* other methods and constructor removed for brevity */


        public void Reset()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }

    }
}
