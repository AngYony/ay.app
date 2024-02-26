using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WY.Application.Customers.Dtos
{
    public class CustomerLoginInputDto
    {
        [Required]
        [JsonPropertyName("customerno")]
        public string CustomerNo { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
