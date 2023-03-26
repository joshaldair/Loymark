
namespace Application.Features.Activities.Queries.DTO;

public class ActivityDTO
{
    public int UserId { get; set; }
    public string ActivityName { get; set; }
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
