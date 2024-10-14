using System.Linq;
using System.Windows;
using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;

namespace AnnuaireEntreprise
{
    public partial class ManageServicesWindow : Window
    {
        private AnnuaireContext _context;

        public ManageServicesWindow()
        {
            InitializeComponent();
            _context = new AnnuaireContext();
            LoadServices();
        }

        private void LoadServices()
        {
            ServicesListView.ItemsSource = _context.Services.ToList();
        }

        private void OnAddServiceButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ServiceTextBox.Text))
            {
                var service = new Service { Nom = ServiceTextBox.Text };
                _context.Services.Add(service);
                _context.SaveChanges();
                LoadServices();
                ServiceTextBox.Clear();
            }
        }

        private void OnEditServiceButtonClick(object sender, RoutedEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService && !string.IsNullOrEmpty(ServiceTextBox.Text))
            {
                selectedService.Nom = ServiceTextBox.Text;
                _context.SaveChanges();
                LoadServices();
                ServiceTextBox.Clear();
            }
        }

        private void OnDeleteServiceButtonClick(object sender, RoutedEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                _context.Services.Remove(selectedService);
                _context.SaveChanges();
                LoadServices();
            }
        }

        private void OnServiceSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ServicesListView.SelectedItem is Service selectedService)
            {
                ServiceTextBox.Text = selectedService.Nom;
            }
        }
    }
}
