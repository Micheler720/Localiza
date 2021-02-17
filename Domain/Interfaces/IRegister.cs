namespace Domain.Interfaces
{
    public interface IRegister<out R>
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}