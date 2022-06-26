using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.Domain
{
    public partial class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
