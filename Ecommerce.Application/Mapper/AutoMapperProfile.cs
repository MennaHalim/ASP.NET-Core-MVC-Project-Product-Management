using AutoMapper;
using Ecommerce.Dtos.Author;
using Ecommerce.Dtos.Book;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<BookCreateViewModel, Book>().ReverseMap();
            CreateMap<BookDetailsViewModel, Book>().ReverseMap();
            CreateMap<BookListViewModel, Book>().ReverseMap();
            CreateMap<BookEditViewModel, Book>().ReverseMap();
            CreateMap<AuthorListViewModel, Author>().ReverseMap();
        }
    }
}
