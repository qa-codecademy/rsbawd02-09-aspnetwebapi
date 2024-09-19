﻿using SEDC.MoviesApp.Enums;

namespace SEDC.MoviesApp.DTOs;

public class MovieDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public GenreEnum Genre { get; set; }
}
