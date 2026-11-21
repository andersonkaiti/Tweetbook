namespace Tweetbook.src.Infrastructure.Http.Contracts.Responses;

public class PostResponse
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
}
