namespace api_back.Roles.Models
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public void UpdateRole(string name)
        {
            Name = name;
        }
    }

}
