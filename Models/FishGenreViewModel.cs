using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DAL_DataAccessLayer.DTO.Models;

namespace Fishnice.Models
{
    public class FishGenreViewModel
    {
        public List<Fish>? Fishes { get; set; }
        public SelectList? Genres { get; set; }
        public string? FishGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
