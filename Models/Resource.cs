using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaOne
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Url { get; set; }
        public string JWT { get; set; }

        public string? AppInsightsKey { get; set; }

        public List<Tenant>? Tenants { get; set; }
    }
}