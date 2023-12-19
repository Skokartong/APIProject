﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using APIProject.Models;

namespace APIProject.Models
{
    public class InterestLink
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}