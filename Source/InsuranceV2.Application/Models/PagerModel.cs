﻿using System.Collections.Generic;

namespace InsuranceV2.Application.Models
{
    public class PagerModel<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public override string ToString()
        {
            return $"PageSize: {PageSize}, PageNumber: {PageNumber}, TotalPages: {TotalPages}";
        }
    }
}