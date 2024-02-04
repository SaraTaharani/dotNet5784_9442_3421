﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Engineer
    {
        public int Id { get; init; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public EngineerExperience? Level { get; set; }
        public double Cost { get; set; }
        public TaskInEngineer? Task {  get; set; }
        public override string ToString() => $" Id: {Id}, Name: {Name}, Email: {Email}, Level: {Level}, Cost: {Cost}, Task: {Task} ";
    }
}
