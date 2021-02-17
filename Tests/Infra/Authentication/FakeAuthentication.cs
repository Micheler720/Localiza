using Domain.Interfaces;
using Domain.Entities;

namespace Infra.Authentication
{
    public class FakeAuthentication : IToken
    {
        public string TokenGenerateClient(Client client)
        {
            return "token-implementation-fake";
        }

        public string TokenGenerateOperator(Operator op)
        {
            return "token-implementation-fake";
        }
    }
}