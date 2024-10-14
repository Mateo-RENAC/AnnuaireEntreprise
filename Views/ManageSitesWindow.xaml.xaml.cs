using System.Linq;
using System.Windows;
using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;

namespace AnnuaireEntreprise
{
    public partial class ManageSitesWindow : Window
    {
        private AnnuaireContext _context;

        public ManageSitesWindow()
        {
            InitializeComponent();
            _context = new AnnuaireContext();
            LoadSites();
        }

        private void LoadSites()
        {
            SitesListView.ItemsSource = _context.Sites.ToList();
        }

        private void OnAddSiteButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SiteTextBox.Text))
            {
                var site = new Site { Ville = SiteTextBox.Text };
                _context.Sites.Add(site);
                _context.SaveChanges();
                LoadSites();
                SiteTextBox.Clear();
            }
        }

        private void OnEditSiteButtonClick(object sender, RoutedEventArgs e)
        {
            if (SitesListView.SelectedItem is Site selectedSite && !string.IsNullOrEmpty(SiteTextBox.Text))
            {
                selectedSite.Ville = SiteTextBox.Text;
                _context.SaveChanges();
                LoadSites();
                SiteTextBox.Clear();
            }
        }

        private void OnDeleteSiteButtonClick(object sender, RoutedEventArgs e)
        {
            if (SitesListView.SelectedItem is Site selectedSite)
            {
                _context.Sites.Remove(selectedSite);
                _context.SaveChanges();
                LoadSites();
            }
        }

        private void OnSiteSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SitesListView.SelectedItem is Site selectedSite)
            {
                SiteTextBox.Text = selectedSite.Ville;
            }
        }
    }
}

