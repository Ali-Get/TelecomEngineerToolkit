using System;

namespace Toolkit.Infrastructure.Models
{
    public class SavedCalculation
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = "";
        public string ToolName { get; set; } = "";      // e.g. "Link Budget"
        public string InputJson { get; set; } = "";     // JSON of inputs
        public string ResultJson { get; set; } = "";    // JSON of results
        public string Notes { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}