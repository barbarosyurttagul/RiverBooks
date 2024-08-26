using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
  internal static readonly Guid Book1Guid = new Guid("28bf24de-6cc7-4fc0-87c4-b6547ae6fef5");
  internal static readonly Guid Book2Guid = new Guid("0ab9b3de-a34e-4114-9825-3704f9b716b3");
  internal static readonly Guid Book3Guid = new Guid("e7bdd288-b0e2-41e1-8c5d-b76ce2a3335d");
  public void Configure(EntityTypeBuilder<Book> builder)
  {
    builder.Property(b => b.Title)
        .IsRequired()
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    builder.Property(b => b.Author)
        .IsRequired()
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    builder.HasData(GetSampleBookData());
  }

  private IEnumerable<Book> GetSampleBookData()
  {
    var tolkien = "J.R.R Tolkien";
    yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 9.99m);
    yield return new Book(Book2Guid, "The Two Towers", tolkien, 10.99m);
    yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
  }
}
