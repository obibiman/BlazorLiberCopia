namespace Bibliographia.Web.API.Models.DataTransfer.Publisher
{
    public class PublisherDetailsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? CompanyUrl { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    } 
}
