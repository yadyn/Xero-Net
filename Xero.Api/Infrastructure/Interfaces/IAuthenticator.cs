using System;
using System.Threading.Tasks;

namespace Xero.Api.Infrastructure.Interfaces
{
    public interface IAuthenticator
    {
        Task<string> GetSignatureAsync(IConsumer consumer, IUser user, Uri uri, string verb, IConsumer consumer1);
        Task<IToken> GetTokenAsync(IConsumer consumer, IUser user);

        IUser User { get; set; }
    }
}
