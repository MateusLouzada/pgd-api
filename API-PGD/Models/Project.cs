﻿namespace API_PGD.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? MainUserID { get; set; }
    }
}
