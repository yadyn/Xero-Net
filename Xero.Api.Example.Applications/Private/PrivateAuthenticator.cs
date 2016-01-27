﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Infrastructure.OAuth.Signing;

namespace Xero.Api.Example.Applications.Private
{
    public class PrivateAuthenticator : IAuthenticator
    {
        private readonly X509Certificate2 _certificate;

        public PrivateAuthenticator(string certificatePath)
            :this(certificatePath, "")
        {
            
        }

        public PrivateAuthenticator(string certificatePath, string certificatePassword = "")
        {
            _certificate = new X509Certificate2();
            _certificate.Import(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
        }

        public PrivateAuthenticator(X509Certificate2 certificate)
        {
            _certificate = certificate;
        }

        public Task<string> GetSignatureAsync(IConsumer consumer, IUser user, Uri uri, string verb, IConsumer consumer1)
        {
            var token = new Token
            {
                ConsumerKey = consumer.ConsumerKey,
                ConsumerSecret = consumer.ConsumerSecret,
                TokenKey = consumer.ConsumerKey
            };

            return Task.FromResult(RsaSha1Signer.CreateSignature(_certificate, token, uri, verb));
        }

        public X509Certificate Certificate { get { return _certificate; } }

        public Task<IToken> GetTokenAsync(IConsumer consumer, IUser user)
        {
            return null;
        }

        public IUser User { get; set; }
    }
}