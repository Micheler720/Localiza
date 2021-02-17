using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IToken
    {
        string TokenGenerateClient(Client client);
        string TokenGenerateOperator(Operator op);
    }
}