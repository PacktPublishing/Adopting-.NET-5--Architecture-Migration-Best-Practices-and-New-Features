using BookApp.Models;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DAL
{
    public class BooksDBInitialiser: CreateDatabaseIfNotExists<BooksDBContext>
    
    {
        protected override void Seed(BooksDBContext context)
        {
            var books = new List<Book>
            {
                new Book
                {
                    Author="Hammad Arif, Habib Qureshi",
                    DatePublished=new DateTime(2021,1,1),
                    Title="Introducing .NET 5",
                    SubTitle="Building & migrating apps using modern architecture on latest .NET platform",
                    Reviews = new List<BookReview>
                    {
                        new BookReview
                        {
                            Rating=3,
                            Title="Aenean ut est dolor",
                            Review="Aenean ut est dolor. Curabitur in arcu vel quam mattis porta. Integer accumsan dignissim auctor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut nec mauris sit amet velit facilisis elementum eget non dui. Proin sagittis tincidunt libero sit amet interdum. Duis posuere volutpat purus, vel sodales arcu sagittis non. Quisque tincidunt id mi ut posuere. Etiam semper velit non tristique efficitur. Sed consequat lobortis fermentum. Fusce ac dolor tellus"
                        },
                        new BookReview
                        {
                            Rating=5,
                            Title="Lorem ipsum dolor sit amet",
                            Review="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."
                        }
                    }
                },
                new Book
                {
                    Author="Sebastian Raschka, Vahid Mirjalili",
                    DatePublished= new DateTime(2019,12,1),
                    Title="Python Machine Learning",
                    SubTitle="Machine learning and deep learning with Python",
                    CoverImage=Utilities.ExtractImage("pythonmachinelearning.png"),
                    Reviews = new List<BookReview>
                    {
                        new BookReview
                        {
                            Rating=3,
                            Title="Aenean ut est dolor",
                            Review="Aenean ut est dolor. Curabitur in arcu vel quam mattis porta. Integer accumsan dignissim auctor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut nec mauris sit amet velit facilisis elementum eget non dui. Proin sagittis tincidunt libero sit amet interdum. Duis posuere volutpat purus, vel sodales arcu sagittis non. Quisque tincidunt id mi ut posuere. Etiam semper velit non tristique efficitur. Sed consequat lobortis fermentum. Fusce ac dolor tellus"
                        },
                        new BookReview
                        {
                            Rating=5,
                            Title="Lorem ipsum dolor sit amet",
                            Review="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."
                        }
                    }
                },
                new Book
                {
                    Author="Matthew Weston",
                    DatePublished= new DateTime(2019,11,1),
                    Title="Learn Microsoft PowerApps",
                    SubTitle="Build customised business applications without writing any code",
                    CoverImage=Utilities.ExtractImage("learnpowerapps.png"),
                    Reviews=new List<BookReview>
                    {
                        new BookReview
                        {
                            Rating=4,
                            Title="Lorem ipsum dolor sit amet",
                            Review="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean congue nec ex at tincidunt. Phasellus vel porta massa. Suspendisse id orci at ex faucibus dictum. Mauris rhoncus nec leo in congue. Donec eu neque sagittis est iaculis interdum. Mauris laoreet elit non blandit tempor. Nunc elementum est sed lorem ultrices, sed tincidunt leo mattis."
                        }

                    }
                }
            };

            context.Books.AddRange(books);
            base.Seed(context);

        }

    }
}
