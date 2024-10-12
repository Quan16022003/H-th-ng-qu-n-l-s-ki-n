namespace Constracts
{
	public class AccountDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Balance { get; set; }
		public Guid OwnerId { get; set; }
	}
}