using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication71.DTOs.Movies
{
    public class GetMoviesDto : BaseSearchModel<GetMovieDto>
    {
        public List<GetMovieDto> Movies { get; set; } = new List<GetMovieDto>();
        public SelectList SortowanieOptionItems { get; set; }
    }
}
