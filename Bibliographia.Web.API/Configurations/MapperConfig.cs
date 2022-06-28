using AutoMapper;
using Bibliographia.Web.API.Models.Domain;
using Bibliographia.Web.API.Models.DataTransfer.Author;
using Bibliographia.Web.API.Models.DataTransfer.Book;
using Bibliographia.Web.API.Models.DataTransfer.Publisher;

namespace Bibliographia.Web.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //mapping for Author
            _ = CreateMap<AuthorCreateDto, Author>().ReverseMap();
            _ = CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            _ = CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();

            //mapping for Book

            //CreateMap<BookReadOnlyDto, Book>().ReverseMap();
            _ = CreateMap<Book, BookReadOnlyDto>()
                .ForMember(z => z.AuthorName, y => y.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
                .ReverseMap();
            _ = CreateMap<Book, BookDetailsDto>()
           .ForMember(z => z.AuthorName, y => y.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
           .ReverseMap();
            _ = CreateMap<BookUpdateDto, Book>().ReverseMap();
            _ = CreateMap<BookCreateDto, Book>().ReverseMap();

            //mapping for Publisher
            _ = CreateMap<PublisherCreateDto, Publisher>().ReverseMap();
            _ = CreateMap<PublisherUpdateDto, Publisher>().ReverseMap();
            _ = CreateMap<PublisherReadOnlyDto, Publisher>().ReverseMap();

            // //mapping for User
            // _ = CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
