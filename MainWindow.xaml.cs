using System;
using System.Linq;
using System.Windows;
using JPO.Data.Entities;
using JPO.Data.Persistence;
using JPO.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace JPO
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _db;

        public MainWindow()
        {
            InitializeComponent();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=jpo.db")
                .Options;

            _db = new AppDbContext(options);

            // Crée la base si elle n’existe pas
            _db.Database.EnsureCreated();

            // si la base est vide
            if (!_db.Events.Any())
            {
                var ev1 = new EventJpo
                {
                    Title = "Visite guidée",
                    Date = new DateTime(2026, 2, 4),
                    Location = "Polytech Nancy",
                    Description = "Départ hall du bâtiment E"
                };

                var ev2 = new EventJpo
                {
                    Title = "TP Robotique",
                    Date = new DateTime(2026, 2, 4),
                    Location = "C244",
                    Description = "Découverte du robot Wifibot"
                };

                _db.Events.AddRange(ev1, ev2);

                _db.Students.AddRange(
                    new Student { FullName = "Louis", Track = "EMME", StudyYear = 5, EventJpo = ev1 },
                    new Student { FullName = "Idriss", Track = "IA2R", StudyYear = 4, EventJpo = ev2 }
                );

                _db.SaveChanges();
            }

            var dialog = new JPO.Services.DialogService();
            DataContext = new MainViewModel(_db, dialog);
        }
    }
}
