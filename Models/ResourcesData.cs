using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ResourcesWebApi.Models
{
    public class ResourcesData
    {
        [Key]
        public int Resourceid { get; set; }
        public string clientId{ get; set; }
        public string tenantId { get; set; }
        public string objectId { get; set; }
         public string subscriptionId { get; set; }
        public string clientSecret { get; set; }
    }
}