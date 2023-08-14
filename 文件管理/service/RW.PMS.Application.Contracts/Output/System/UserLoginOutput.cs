namespace RW.PMS.Application.Contracts.Output.System
{
    public class UserLoginOutput
    {
        public int Id { get; set; }

        public string RealName { get; set; }

        public int[] Role { get; set; }
    }
}