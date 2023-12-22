using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using APIProject.Models;
using APIProject.Data;
using System.Linq;
using System;

namespace APIProject.Models
{
    public class InterestLink
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public virtual Person Person { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
