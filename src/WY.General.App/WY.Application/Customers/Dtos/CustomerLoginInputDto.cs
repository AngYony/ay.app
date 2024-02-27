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
        /// <summary>
        /// 用户编码
        /// </summary>
        [Required]
        [JsonPropertyName("customerno")]
        public string CustomerNo { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
