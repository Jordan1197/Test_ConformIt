using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Authentication;
using TestProgrammationConformit.Authorization;

namespace TestProgrammationConformit.Authentication
{
    public class InMemoryGetApiKeyQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryGetApiKeyQuery()
        {
            var existingApiKeys = new List<ApiKey>
            {
                new ApiKey(1,"Admin","78A51A29D7821B48E8FE2F5C63DE1",
                    DateTime.Now,
                    new List<string>
                    {
                        Roles.Admin,
                        Roles.User
                    }),
                new ApiKey(2,"User","9772E796F57E82AEF1F414A1C3BA2",
                    DateTime.Now,
                    new List<string>
                    {
                        Roles.User
                    })
            };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
