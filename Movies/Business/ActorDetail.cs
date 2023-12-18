using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movies.Models;

namespace Movies.Business
{
    public class ActorDetail
    {
        public int ActorId { get; set; }
        public string? Image { get; set; }
        public string? NameActor { get; set; }
        public string? NationId { get; set; }
        public string? NationName { get; set; }
        public string? DoB { get; set; }
    }
}
