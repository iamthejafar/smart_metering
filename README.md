# smart_metering


## Configuration

In DataContext.cs file change path to your local path of database file.

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
      optionsBuilder.UseSqlite("Data Source=YOUR_LOCAL_PATH");
  }
